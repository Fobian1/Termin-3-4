#include <iostream>

int main(int numOfArgs, char* pointersArgs[])
{
	std::cout << "Hello World! Nice to see you " << std::endl;
	for (int i = 1; i < numOfArgs; i++) {
		std::cout << i << ":" << pointersArgs[i];
	}

	return 0;
}