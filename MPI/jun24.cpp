// MPI_DS.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include <cstdlib>
#include <iostream>
#include <mpi.h>
#define NUM_COUNT 105

int main(int argc, char** argv)
{
    int rank, n;
	int* buf;
	int* rbuf;
    MPI_File fh; 
	MPI_Datatype filetype;
	MPI_Offset off;

	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &n);

	buf = (int*)malloc(NUM_COUNT*sizeof(int));
	rbuf = (int*)malloc(NUM_COUNT * sizeof(int));

	for (int i = 0; i < NUM_COUNT; i++)
		buf[i] = rank * NUM_COUNT + i;

	off = (MPI_Offset)(n - 1 - rank)*NUM_COUNT*sizeof(int);

	MPI_File_open(MPI_COMM_WORLD, "file1.txt", MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &fh);
	MPI_File_seek(fh, off, MPI_SEEK_SET);
	MPI_File_write(fh, buf, NUM_COUNT, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&fh);

	MPI_File_open(MPI_COMM_WORLD, "file1.txt", MPI_MODE_RDONLY, MPI_INFO_NULL, &fh);
	MPI_File_read_at(fh, off, rbuf, NUM_COUNT, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&fh);

	int blocks = 0;
	int nums = NUM_COUNT;
	while (nums > 0)
	{
		blocks++;
		nums -= blocks;
	}


	int* blengths = (int*)malloc(blocks * sizeof(int));
	int* displs = (int*)malloc(blocks * sizeof(int));
	for (int i = 0; i < blocks; i++)
	{
		blengths[i] = i + 1;
		displs[i] = n * i * (i + 1) / 2 + rank * (i + 1);
	}

	MPI_Type_indexed(blocks, blengths, displs, MPI_INT, &filetype);
	MPI_Type_commit(&filetype);

	MPI_File_open(MPI_COMM_WORLD, "file2.txt", MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &fh);
	MPI_File_set_view(fh, 0, MPI_INT, filetype, "native", MPI_INFO_NULL);
	MPI_File_write_all(fh, rbuf, NUM_COUNT, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&fh);

	MPI_Type_free(&filetype);
	free(blengths);
	free(displs);
	free(buf);
	free(rbuf);
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
