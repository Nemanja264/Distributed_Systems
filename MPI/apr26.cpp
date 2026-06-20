// MPI_DS.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include <cstdlib>
#include <iostream>
#include <mpi.h>
#define FILE_SIZE 1024*1024

int main(int argc, char** argv)
{
    int rank, n;
	unsigned char* buf;
	//int* rbuf;
    MPI_File fh; 
	MPI_Datatype filetype;
	MPI_Offset off;

	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &n);

	int bufsize = FILE_SIZE / n;
	off = (MPI_Offset)(n - 1 - rank) * bufsize;

	buf = (unsigned char*)malloc(bufsize);

	MPI_File_open(MPI_COMM_WORLD, "input.dat", MPI_MODE_RDONLY, MPI_INFO_NULL, &fh);
	MPI_File_seek(fh, off, MPI_SEEK_SET);
	MPI_File_read(fh, buf, bufsize * sizeof(int), MPI_BYTE, MPI_STATUSES_IGNORE);
	MPI_File_close(&fh);

	int part = bufsize / 2;
	MPI_Type_vector(2, part, part*n, MPI_BYTE, &filetype);
	MPI_Type_commit(&filetype);

	MPI_File_open(MPI_COMM_WORLD, "april.dat", MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &fh);
	MPI_File_set_view(fh, (MPI_Offset)rank * part, MPI_BYTE, filetype, "native", MPI_INFO_NULL);
	MPI_File_write_all(fh, buf, bufsize, MPI_BYTE, MPI_STATUSES_IGNORE);
	MPI_File_close(&fh);

	MPI_Type_free(&filetype);
	free(buf);
	//free(rbuf);
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
