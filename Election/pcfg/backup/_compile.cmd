SET QAS_DIR=%1
SHIFT
%QAS_DIR%\mlgtools.exe feat2proc fsrc=lexicons.txt fbin=%1.%2.matches0.bin
%QAS_DIR%\mlgtools.exe feat2proc fsrc=stopwords.txt fbin=%1.%2.stopwords.bin
perl.exe template\gen.pl %1 %2 %3 %4

