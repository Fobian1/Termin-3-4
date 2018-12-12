#ifndef POLY2_H
#define POLY2_H
#include <string>

class Poly2
{
	float a, b, c;
	std::string errorMessage;

public:

    /*
     * Constructor
     */
	Poly2(float a, float b, float c);

    /*
     * Evaluate y(x)
     */
	float eval(float x);

    /*
     * Find roots, i.e. x when y(x) = 0
     */
	void findRoots(float &root1, float &root2);
	void ReadCoefficients(float &a, float &b, float &c);
	void SolveLab1_11();
    /*
     * Destructor:
     * not needed since no dynamic memory has been allocated,
     */
};

#endif