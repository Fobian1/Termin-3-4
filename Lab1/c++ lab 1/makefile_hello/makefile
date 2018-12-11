
# Definitions
TARGET = hello.exe
SOURCES = hello.cpp

# Default target (the 'all' pseudo-target)
all: $(TARGET)

# Remove intermediates and executables, if they exist
clean:
 @if exist *.obj del *.obj
 @if exist $(TARGET) del $(TARGET)

# Build target
# Last line evaluates to: cl /EHsc /Fe:hello.exe hello.cpp
$(TARGET):$(SOURCES)
 cl /EHsc /Fe:$@ $**