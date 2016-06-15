#!perl -w

($#ARGV != 4) || die "Usage: $0 classifier_name classifier_version classifier_QLF";

$classifiername = $ARGV[0] . '.' . $ARGV[1] . '.';

$filenameregex = "^___\.(.*)";
$classifiernamereplace = "#CLASSIFIERNAME#";
$classifierversionreplace = "#CLASSIFIERVERSION#";
$classifierdatasetreplace = "#DATASET#";
$classifierqlfmsidreplace = "#MSID#";

opendir(DIR, '.\template') || die "Can't open current dir";

@files = readdir(DIR);
closedir DIR;

foreach (@files)
{
	if (/$filenameregex/)
	{
		$templatename = '.\template\\' . $_;
		$filename = $classifiername . $1 . "\n";
		open(INFILE, "$templatename") || die ("Could not open <templatename>");
		open(OUTFILE, ">$filename") || die ("Could not open <$filename>");
		@_ = <INFILE>;
		foreach (@_)
		{
			s/$classifiernamereplace/$ARGV[0]/g;
			s/$classifierversionreplace/$ARGV[1]/g;
			s/$classifierdatasetreplace/$ARGV[2]/g;
			s/$classifierqlfmsidreplace/$ARGV[3]/g;
			print OUTFILE $_;
		}
		close(INFILE);
		close(OUTFILE);
	}
}