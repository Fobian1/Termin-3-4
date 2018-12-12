#include "EratosthenesSieve.h"
using namespace std;
#include "stdafx.h"

//Constructor - körs alltid först
EratosthenesSieve::EratosthenesSieve()
{
	//Arrayn får konstiga värden
	//Initiera: fyll alla element med 0
	//1.
	Init();
	//2.
	//list num fr 2 till maxNum, stryk de jämna	
	//spara värden  i arrayen numberList
	generateNumberList();
	//Test printa ut talen	
	printAllNumbers();

	//3. Räkna primtal och spara i arrayen primeList
	generatePrimeNumbers();
	//lista ut primtalen
	printPrimeNumbers();
}

//Initiera: nollställ alla instansvariabler
//Initiera: fyll alla element med 0
void EratosthenesSieve::Init()
{
	//Gör alla nollställningar
	for (int i = 0; i < maxNum;i++)
	{
		numberList[i] = 0;
		primeList[i] = 0;
	}

}
void EratosthenesSieve::generateNumberList()
{
	//spara number från 2 till n i numberlist - bara udda
	numberList[0] = 2; //talet börjar med 2
	int j = 1;  //räknare för tal som är udda

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
	int i = 0;  //räknare for numerList
	int j = 0;  //räknare för primenumbers
	
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
