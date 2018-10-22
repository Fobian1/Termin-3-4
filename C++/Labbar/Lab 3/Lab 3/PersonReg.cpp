#include "pch.h"
#include "PersonReg.h"
#include <iostream>

using namespace std;

class PersonReg {
public:
	int maxSize;
	void SetSize(int maxSize);
	int GetSize(void);
	PersonReg();

};

PersonReg::PersonReg(void) {
}

void PersonReg::SetSize(int maxSize) {

}





int Main() {
	PersonReg personReg;

	personReg.SetSize(10.0);
}
