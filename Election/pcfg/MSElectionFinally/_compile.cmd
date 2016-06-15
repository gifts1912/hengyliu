SET QAS_DIR=%1
SHIFT
perl.exe template\gen.pl %1 %2 %3 %4
