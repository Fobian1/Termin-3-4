#include <iostream>
#include "poly2.h"

int main(int argc, char** argv)
{
	std::cout << "Root-finding started..." << std::endl;

	Poly2 h = Poly2(1, 2, 1);
	h.findRoots();
	h.eval(1);
    
	Poly2 solver = Poly2(2, -1, -1);
	solver.findRoots();

	Poly2 solver2 = Poly2(1,1,1);
	solver2.findRoots();

	return 0;
}