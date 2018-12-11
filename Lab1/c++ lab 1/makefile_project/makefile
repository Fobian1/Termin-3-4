
# Definitions
DIR_SRC = src
DIR_INCLUDE = include
DIR_BIN = bin
TARGET = hellopi.exe
SOURCES = $(DIR_SRC)\main.cpp

# Clean up
clean:
 @if exist $(DIR_BIN) rmdir /S /Q $(DIR_BIN)

# Build target
$(TARGET):$(SOURCES)
 cl /EHsc /Fe:$@ /I $(DIR_INCLUDE) $**
 copy *.obj $(DIR_BIN)
 del *.obj
 copy $(TARGET) $(DIR_BIN)
 del $(TARGET)

create_dirs:
 @if not exist $(DIR_BIN) mkdir $(DIR_BIN)

# Clean up, create bin folder and build everything
all: clean create_dirs $(TARGET)