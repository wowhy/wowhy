REFERENCE = System.Data.dll
BIN = bin

all:HyLibrary.dll Program01.exe

HyLibrary.dll: test
	mcs -t:library -o+ -out:bin/HyLibrary.dll -langversion:Future -sdk:4.5 \
		-nowarn:1591,1572,1573 \
		-r:$(REFERENCE) \
		-recurse:HyLibrary/*.cs \
		-doc:bin/HyLibrary.xml

Program01.exe: test
	test -d $(BIN) || mkdir $(BIN)
	mcs -t:exe -o+ -out:bin/Program01.exe -langversion:Future -sdk:4.5 \
		-r:bin/HyLibrary.dll \
		-recurse:Program01/*.cs

test:
	test -d $(BIN) || mkdir $(BIN)

clean:
	rm -f bin/*.exe
	rm -f bin/*.dll