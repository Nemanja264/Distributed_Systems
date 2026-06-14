// MPI_DS.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include <cstdlib>
#include <iostream>
#include <mpi.h>
#define FILESIZE (10*1024*1024) // 10MB

int main(int argc, char** argv)
{
    int rank, n, bufsize, part;
	unsigned char* buf;
    MPI_File fh; 
	MPI_Datatype filetype;

	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &n);

	bufsize = FILESIZE / n;
	part = bufsize / 4;
	buf = (unsigned char*)malloc(bufsize);

	int rc = MPI_File_open(MPI_COMM_WORLD, "input.dat", MPI_MODE_RDONLY, MPI_INFO_NULL, &fh);
	if (rc != MPI_SUCCESS) {
		char msg[MPI_MAX_ERROR_STRING]; int len;
		MPI_Error_string(rc, msg, &len);
		printf("rank %d: open failed: %s\n", rank, msg);
		MPI_Abort(MPI_COMM_WORLD, 1);
	}

	MPI_File_seek(fh, (MPI_Offset)(n-1-rank)*bufsize, MPI_SEEK_SET);
	MPI_File_read(fh, buf, bufsize, MPI_BYTE, MPI_STATUS_IGNORE);
	MPI_File_close(&fh);

	MPI_Type_vector(4, part, n*part, MPI_BYTE, &filetype);
	MPI_Type_commit(&filetype);

	MPI_File_open(MPI_COMM_WORLD, "output.dat", MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &fh);
	MPI_File_set_view(fh, (MPI_Offset)(rank * part), MPI_BYTE, filetype, "native", MPI_INFO_NULL);
	MPI_File_write_all(fh, buf, bufsize, MPI_BYTE, MPI_STATUS_IGNORE);
	MPI_File_close(&fh);

	MPI_Type_free(&filetype);
	free(buf);
	MPI_Finalize();
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
