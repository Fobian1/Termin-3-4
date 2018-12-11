#include <iostream>
#include "poly2.h"
#include <fstream>

using namespace std;

int main(int argc, char** argv)
{
	//SolveLab1_11();

	std::cout << "Root-finding started..." << std::endl;

	Poly2 h = Poly2(1, 2, 1);

	float x1, x2;

	h.findRoots(x1, x2);
	std::cout << "root 1 = " << x1 << "\n";
	std::cout << "root 2 = " << x2 << "\n";

	Poly2 obj = Poly2(2, -1, -1);
	obj.findRoots(x1, x2);
	std::cout << "root 1 = " << x1 << "\n";
	std::cout << "root 2 = " << x2 << "\n";
	float res = obj.eval(x1);
	std::cout << "eval = " << res << "\n";
	res = obj.eval(x2);
	std::cout << "eval = " << res << "\n";

	float a, b, c;
	
	obj.ReadCoefficients(a, b, c);
	Poly2 obj10 = Poly2(a, b, c);
	obj10.findRoots(x1, x2);
	std::cout << "root 1 = " << x1 << "\n";
	std::cout << "root 2 = " << x2 << "\n";
	

	return 0;
}

void SolveLab1_11()
{
	//read from a file

	//ifstream inFile;
	//inFile.open("coeffs.txt");
	//ofstream outFile;
	//outFile.open("roots.txt");

	float  a = 0, b = 0, c = 0;
	//inFile >> a, b, c;

	//if (inFile.get(a, b, c)) 
	//{
	//	outFile << a, b, c;
	//}

	//inFile.close();

	Poly2 h = Poly2(a, b, c);

	float x1, x2;
	Poly2 obj = Poly2(a, b, c);
	h.findRoots(x1, x2);
	std::cout << "root 1 = " << x1 << "\n";
	std::cout << "root 2 = " << x2 << "\n";
}

