# FileName Definitions
APPNAME := H2O_Master
BACKEND_UNITTEST_SOURCENAME := UnitTest
EXTERNALLIBRARYFOLDER := $(subst /,\\,$(USERPROFILE))\Documents\C\OneDrive

BACKEND_DIR := Back-End
FRONTEND_DIR := Front-End
#start

# ----------------------------------FontEnd----------------------------------
# Targets
FRONTEND_OUTDIR_BASE := $(FRONTEND_DIR)\bin\Release\net8.0-windows\publish
FRONTEND_X64_PROFILE := Publish_x64
FRONTEND_X86_PROFILE := Publish_x86
FRONTEND_X64_DIR := $(FRONTEND_OUTDIR_BASE)\win-x64
FRONTEND_X86_DIR := $(FRONTEND_OUTDIR_BASE)\win-x86

# Archive names
FRONTEND_COMPRESSEDFILENAME_x64 := $(APPNAME)_x64.7z
FRONTEND_COMPRESSEDFILENAME_x86 := $(APPNAME)_x86.7z
# ---------------------------------------------------------------------------


# ----------------------------------BackEnd----------------------------------
# Directory definitions
BACKEND_BUILD_DIR_x86 := $(BACKEND_DIR)\build\x86
BACKEND_BUILD_DIR_x64 := $(BACKEND_DIR)\build\x64
BACKEND_OBJ_DIR_x86 := $(BACKEND_BUILD_DIR_x86)\objects
BACKEND_OBJ_DIR_x64 := $(BACKEND_BUILD_DIR_x64)\objects

# Compiler paths for different architectures
GCC_x86 := i686-w64-mingw32-gcc
GCC_x64 := x86_64-w64-mingw32-gcc

# Build flags
C_FLAGS_BASE := -std=gnu2x -pedantic -Wall -Wextra -O3 -pipe -flto
C_FLAGS_UNITTEST := $(C_FLAGS_BASE) -ggdb3 -fdiagnostics-color=always
C_FLAGS_LIBDLL := $(C_FLAGS_BASE) -DBUILD_DLL -fvisibility=hidden -fno-exceptions -fno-unwind-tables -fno-asynchronous-unwind-tables -Wl,--strip-all -shared -fPIC

# Include paths
INCLUDE_HEADER_SEARCH_DIRECTORIES := \
-I$(BACKEND_DIR) \
-I$(BACKEND_DIR)\includes \
-I$(EXTERNALLIBRARYFOLDER)

# C source files
LINKED_SRC := \
$(subst /,\\,$(wildcard $(subst \\,/,$(BACKEND_DIR))/linked/*.c)) \
$(EXTERNALLIBRARYFOLDER)\Cryptography\CRC.c \
$(EXTERNALLIBRARYFOLDER)\DynamicDataStructures\BinaryBuilder.c \
$(EXTERNALLIBRARYFOLDER)\DynamicDataStructures\StringBuilder.c

# Main source files
BACKEND_LIBDLL_SRC := $(BACKEND_DIR)\$(APPNAME).c
BACKEND_UNITTEST_SRC := $(BACKEND_DIR)\$(BACKEND_UNITTEST_SOURCENAME).c

# Generate object file lists
BACKEND_LIBDLL_OBJS_x86 := $(patsubst %.c,$(BACKEND_OBJ_DIR_x86)\\%.o,$(notdir $(LINKED_SRC) $(BACKEND_LIBDLL_SRC)))
BACKEND_LIBDLL_OBJS_x64 := $(patsubst %.c,$(BACKEND_OBJ_DIR_x64)\\%.o,$(notdir $(LINKED_SRC) $(BACKEND_LIBDLL_SRC)))

BACKEND_UNITTEST_OBJS_x86 := $(patsubst %.c,$(BACKEND_OBJ_DIR_x86)\\unittest_%.o,$(notdir $(LINKED_SRC) $(BACKEND_UNITTEST_SRC)))
BACKEND_UNITTEST_OBJS_x64 := $(patsubst %.c,$(BACKEND_OBJ_DIR_x64)\\unittest_%.o,$(notdir $(LINKED_SRC) $(BACKEND_UNITTEST_SRC)))
# --------------------------------------------------------------------------


# Default target - Library DLL build
.PHONY: all libdll app app64 app86 test clean-all clean-lib clean-x86 clean-x64 clean-app

all: libdll app

app: app64 app86

# Publish and compress 64-bit
app64:
	dotnet publish -p:PublishProfile=$(FRONTEND_X64_PROFILE)
	-del /q /f $(FRONTEND_COMPRESSEDFILENAME_x64)
	cd "$(FRONTEND_X64_DIR)" && 7z a -t7z $(FRONTEND_COMPRESSEDFILENAME_x64) *
	move /y ".\$(FRONTEND_X64_DIR)\$(FRONTEND_COMPRESSEDFILENAME_x64)" .

# Publish and compress 32-bit
app86:
	dotnet publish -p:PublishProfile=$(FRONTEND_X86_PROFILE)
	-del /q /f $(FRONTEND_COMPRESSEDFILENAME_x86)
	cd "$(FRONTEND_X86_DIR)" && 7z a -t7z $(FRONTEND_COMPRESSEDFILENAME_x86) *
	move /y ".\$(FRONTEND_X86_DIR)\$(FRONTEND_COMPRESSEDFILENAME_x86)" .

libdll: $(BACKEND_BUILD_DIR_x86)\$(APPNAME)_Library.dll $(BACKEND_BUILD_DIR_x64)\$(APPNAME)_Library.dll

test: $(BACKEND_BUILD_DIR_x86)\$(BACKEND_UNITTEST_SOURCENAME).exe $(BACKEND_BUILD_DIR_x64)\$(BACKEND_UNITTEST_SOURCENAME).exe

# Create directories
$(BACKEND_OBJ_DIR_x86) $(BACKEND_OBJ_DIR_x64) $(BACKEND_BUILD_DIR_x86) $(BACKEND_BUILD_DIR_x64):
	@if not exist "$(subst /,\,$@)" mkdir "$(subst /,\,$@)"

# x86 Library DLL DLL
$(BACKEND_BUILD_DIR_x86)\$(APPNAME)_Library.dll: $(BACKEND_LIBDLL_OBJS_x86) | $(BACKEND_BUILD_DIR_x86)
	$(GCC_x86) $(C_FLAGS_LIBDLL) $(INCLUDE_HEADER_SEARCH_DIRECTORIES) -o $@ $^

# x64 Library DLL DLL
$(BACKEND_BUILD_DIR_x64)\$(APPNAME)_Library.dll: $(BACKEND_LIBDLL_OBJS_x64) | $(BACKEND_BUILD_DIR_x64)
	$(GCC_x64) $(C_FLAGS_LIBDLL) $(INCLUDE_HEADER_SEARCH_DIRECTORIES) -o $@ $^

# x86 Unit Test Executable
$(BACKEND_BUILD_DIR_x86)\$(BACKEND_UNITTEST_SOURCENAME).exe: $(BACKEND_UNITTEST_OBJS_x86) | $(BACKEND_BUILD_DIR_x86)
	$(GCC_x86) $(C_FLAGS_UNITTEST) $(INCLUDE_HEADER_SEARCH_DIRECTORIES) -o $@ $^

# x64 Unit Test Executable
$(BACKEND_BUILD_DIR_x64)\$(BACKEND_UNITTEST_SOURCENAME).exe: $(BACKEND_UNITTEST_OBJS_x64) | $(BACKEND_BUILD_DIR_x64)
	$(GCC_x64) $(C_FLAGS_UNITTEST) $(INCLUDE_HEADER_SEARCH_DIRECTORIES) -o $@ $^

# Library DLL object file compilation rules for x86
define LIBDLL_OBJ_x86_template
$(BACKEND_OBJ_DIR_x86)\$(notdir $(basename $(1))).o: $(1) | $(BACKEND_OBJ_DIR_x86)
	$(GCC_x86) $(C_FLAGS_LIBDLL) $(INCLUDE_HEADER_SEARCH_DIRECTORIES) -c "$(1)" -o "$$@"
endef

$(foreach src,$(LINKED_SRC) $(BACKEND_LIBDLL_SRC),$(eval $(call LIBDLL_OBJ_x86_template,$(src))))

# Library DLL object file compilation rules for x64
define LIBDLL_OBJ_x64_template
$(BACKEND_OBJ_DIR_x64)\$(notdir $(basename $(1))).o: $(1) | $(BACKEND_OBJ_DIR_x64)
	$(GCC_x64) $(C_FLAGS_LIBDLL) $(INCLUDE_HEADER_SEARCH_DIRECTORIES) -c "$(1)" -o "$$@"
endef

$(foreach src,$(LINKED_SRC) $(BACKEND_LIBDLL_SRC),$(eval $(call LIBDLL_OBJ_x64_template,$(src))))

# Unit test object file compilation rules for x86
define UNITTEST_OBJ_x86_template
$(BACKEND_OBJ_DIR_x86)\unittest_$(notdir $(basename $(1))).o: $(1) | $(BACKEND_OBJ_DIR_x86)
	$(GCC_x86) $(C_FLAGS_UNITTEST) $(INCLUDE_HEADER_SEARCH_DIRECTORIES) -c "$(1)" -o "$$@"
endef

$(foreach src,$(LINKED_SRC) $(BACKEND_UNITTEST_SRC),$(eval $(call UNITTEST_OBJ_x86_template,$(src))))

# Unit test object file compilation rules for x64
define UNITTEST_OBJ_x64_template
$(BACKEND_OBJ_DIR_x64)\unittest_$(notdir $(basename $(1))).o: $(1) | $(BACKEND_OBJ_DIR_x64)
	$(GCC_x64) $(C_FLAGS_UNITTEST) $(INCLUDE_HEADER_SEARCH_DIRECTORIES) -c "$(1)" -o "$$@"
endef

$(foreach src,$(LINKED_SRC) $(BACKEND_UNITTEST_SRC),$(eval $(call UNITTEST_OBJ_x64_template,$(src))))



# Clean targets
clean-all: clean-lib clean-app

clean-lib: clean-x86 clean-x64

clean-x86:
	@if exist "$(subst /,\,$(BACKEND_BUILD_DIR_x86))" rmdir /s /q "$(subst /,\,$(BACKEND_BUILD_DIR_x86))"

clean-x64:
	@if exist "$(subst /,\,$(BACKEND_BUILD_DIR_x64))" rmdir /s /q "$(subst /,\,$(BACKEND_BUILD_DIR_x64))"

clean-app:
	-del /q /s $(FRONTEND_X64_DIR)\* 2>NUL
	-del /q /s $(FRONTEND_X86_DIR)\* 2>NUL
	-del /q $(FRONTEND_COMPRESSEDFILENAME_x64) $(FRONTEND_COMPRESSEDFILENAME_x86) 2>NUL

# Help target
help:
	@echo "Available targets:"
	@echo "  all        - Build Library DLL DLLs (default)"
	@echo "  libdll 	- Build library dll for both x86 and x64"
	@echo "  app 		- Build application for both x86 and x64"
	@echo "  app64 		- Build application for x64"
	@echo "  app86 		- Build application for x86"
	@echo "  test       - Build UnitTest for both x86 and x64"
	@echo "  clean-all  - Clean all build artifacts"
	@echo "  clean-lib  - Clean all back-end build artifacts"
	@echo "  clean-x86  - Clean x86 back-end build artifacts"
	@echo "  clean-x64  - Clean x64 back-end build artifacts"
	@echo "  clean-app  - Clean x64 front-end build artifacts"
	@echo "  help       - Show this help message"

# Dependency tracking - automatically handle header changes
-include $(BACKEND_LIBDLL_OBJS_x86:.o=.d)
-include $(BACKEND_LIBDLL_OBJS_x64:.o=.d)
-include $(BACKEND_UNITTEST_OBJS_x86:.o=.d)
-include $(BACKEND_UNITTEST_OBJS_x64:.o=.d)