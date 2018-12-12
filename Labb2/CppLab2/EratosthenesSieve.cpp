#include "EratosthenesSieve.h"
using namespace std;
#include "stdafx.h"

//Constructor - k�rs alltid f�rst
EratosthenesSieve::EratosthenesSieve()
{
	//Arrayn f�r konstiga v�rden
	//Initiera: fyll alla element med 0
	//1.
	Init();
	//2.
	//list num fr 2 till maxNum, stryk de j�mna	
	//spara v�rden  i arrayen numberList
	generateNumberList();
	//Test printa ut talen	
	printAllNumbers();

	//3. R�kna primtal och spara i arrayen primeList
	generatePrimeNumbers();
	//lista ut primtalen
	printPrimeNumbers();
}

//Initiera: nollst�ll alla instansvariabler
//Initiera: fyll alla element med 0
void EratosthenesSieve::Init()
{
	//G�r alla nollst�llningar
	for (int i = 0; i < maxNum;i++)
	{
		numberList[i] = 0;
		primeList[i] = 0;
	}

}
void EratosthenesSieve::generateNumberList()
{
	//spara number fr�n 2 till n i numberlist - bara udda
	numberList[0] = 2; //talet b�rjar med 2
	int j = 1;  //r�knare f�r tal som �r udda

	for (int i = 3; i < maxNum; i++)
	{	
		if (i % 2 != 0)  //om udda 
		{
			numberList[j++] = i;  //udda nummer, j= 1,2 .. 
		}
	}
	numOfValues = j;  //bara udda och 2
}
void EratosthenesSieve::generatePrimeNumbers()
{	
	int i = 0;  //r�knare for numerList
	int j = 0;  //r�knare f�r primenumbers
	
	for (i = 0; i <numOfValues-1; i++) 
	{
		if (numberList[i] == 0)
			continue;

		primeList[j] = numberList[i];

		for (int p = i + 1; p < numOfValues; p++)
		{
			if ( numberList[p] > 0 ) 
			{
				if (numberList[p] % primeList[j] == 0)
				{
					numberList[p] = 0;  //stryk  markera med 0
				}
			}
		}
		j++;		
	}	
	numOfPrimes = j;
}

void EratosthenesSieve::printAllNumbers()
{
	cout << " " << endl;  //tomrad
	cout << " ********* All Numbers ********" << endl;
	cout << "Num of values < " << maxNum << " generated = " << numOfValues << endl;

	for (int i = 0; i < numOfValues; i++)
	{
		//if (numberList[i] > 0)
			cout << numberList[i]<< ", ";		
	}
	cout << endl;
}
void EratosthenesSieve::printPrimeNumbers()
{
	cout << " " << endl;
	cout << " ********* Prime Numbers ********" << endl;

	cout << "Num of primes found: "<< numOfPrimes << endl;

	for (int i = 0; i < numOfPrimes; i++)
	{
		cout << primeList[i] << ", ";
	}
	cout << endl;	
}
EratosthenesSieve::~EratosthenesSieve()
{

}
