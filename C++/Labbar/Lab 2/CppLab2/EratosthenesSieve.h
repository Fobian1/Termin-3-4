#ifndef ERATOSH_H
#define ERATOSH_H

#include <iostream>
#include "stdafx.h"

class EratosthenesSieve
{
	const int maxNum = 50;
	int numOfValues = 0;
	int numOfPrimes = 0;
	int numberList[50];
	int primeList[50];

public:
	void Init();
	void generateNumberList();
	void generatePrimeNumbers();
	void printPrimeNumbers();
	void printAllNumbers();

	EratosthenesSieve();
	~EratosthenesSieve();


};
#endif


