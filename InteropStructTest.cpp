// InteropStructTest.cpp : Defines the exported functions for the DLL application.
// This is the source code for InteropStructTest.dll.

#include "stdafx.h"
#include <stdio.h>

#ifdef INTEROPSTRUCTTEST_EXPORTS
#define INTEROPSTRUCTTEST_API __declspec(dllexport)
#else
#define INTEROPSTRUCTTEST_API __declspec(dllimport)
#endif

extern "C"{
	INTEROPSTRUCTTEST_API int GetInteger();
	INTEROPSTRUCTTEST_API void PassIntegerPointer(int *i);
	INTEROPSTRUCTTEST_API void PassSimpleStructPointer(struct SimpleStruct *s);
	INTEROPSTRUCTTEST_API void PassSimpleStructArray(int size, struct SimpleStruct *s);
	INTEROPSTRUCTTEST_API void PassComplexStructPointer(struct ComplexStruct *s);
	INTEROPSTRUCTTEST_API void PassComplexStructArray(int size, struct ComplexStruct *s);
	INTEROPSTRUCTTEST_API void DebugCTypeSizes();

	struct SimpleStruct
	{
		int firstInt;
		int secondInt;
	};

	struct ComplexStruct
	{
		char firstChar;
		char secondChar;
		unsigned long firstLong;
		short firstShort;
		char thirdChar;
		char fourthChar;
	};

	INTEROPSTRUCTTEST_API int GetInteger()
	{
		return 42;
	}

	INTEROPSTRUCTTEST_API void PassIntegerPointer(int *i)
	{
		*i = 27;
	}

	INTEROPSTRUCTTEST_API void PassSimpleStructPointer(struct SimpleStruct *s)
	{
		s->firstInt = 42;
		s->secondInt = 27;
	}

	INTEROPSTRUCTTEST_API void PassSimpleStructArray(int size, struct SimpleStruct *s)
	{
		int i = 0;
		int value = 1;
		struct SimpleStruct *sptr;

		sptr = s;

		for (i = 0; i < size; i++)
		{
			sptr->firstInt = value++;
			sptr->secondInt = value++;
			sptr++;
		}
	}
	
	INTEROPSTRUCTTEST_API void PassComplexStructPointer(struct ComplexStruct *s)
	{
		s->firstChar = 1;
		s->secondChar = 2;
		s->firstLong = 3;
		s->firstShort = 4;
		s->thirdChar = 5;
		s->fourthChar = 6;
	}

	INTEROPSTRUCTTEST_API void PassComplexStructArray(int size, struct ComplexStruct *s)
	{
		int i = 0;
		int value = 1;
		struct ComplexStruct *sptr;

		sptr = s;

		for (i = 0; i < size; i++)
		{
			sptr->firstChar = value++;
			sptr->secondChar = value++;
			sptr->firstLong = value++;
			sptr->firstShort = value++;
			sptr->thirdChar = value++;
			sptr->fourthChar = value++;
			sptr++;
		}
	}

	INTEROPSTRUCTTEST_API void DebugCTypeSizes()
	{
		printf("\nSize of C char: %i\n", sizeof(char));
		printf("Size of C short: %i\n", sizeof(short));
		printf("Size of C int: %i\n", sizeof(int));
		printf("Size of C long: %i\n", sizeof(long));
		printf("Size of C SimpleStruct: %i\n", sizeof(struct SimpleStruct));
		printf("Size of C ComplexStruct: %i\n", sizeof(struct ComplexStruct));
	}
}
