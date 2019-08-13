#include "poly2.h"
#include <cmath>
#include <iostream>

Poly2::Poly2(float a, float b, float c)
{
	this->a = a;
	this->b = b;
	this->c = c;
}

float Poly2::eval(float x)
{
	float y = a*x*x+ b*x + c;
	return y;

}

void Poly2::findRoots()
{
	    
	float d = b*b- 4 * a*c;
	
	if (d < 0) 
	{
		errorMessage = "Har ingen lösning!";
		std::cout << errorMessage << "Beräkningen avbryts!"<< "\n";
		return; //avbryt beräkningen
	}
	else if (d == 0.0) 
	{
		float x = -b / (2 * a);
		std::cout << "root 1 = root 2 =  " << x << "\n";
	}
	else //if (d > 0) 
	{
		float root = sqrt(d );
		std::cout << "root = " << root <<  "d = " << d <<"\n";
		float x1 = (-b + root) / (2 * a);
		float x2 = (-b - root) / (2 * a);
		std::cout << "root 1 = " << x1 << "\n";
		std::cout << "root 2 = " << x2 << "\n";
	}

} 