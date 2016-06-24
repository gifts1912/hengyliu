using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Utility
{
    public class MovieCandidateFeature
    {
        public MovieCandidateFeature() { }
        public MovieCandidateFeature(int maxPosition) 
        {
            ProdTopWebPos = maxPosition + 2;
            ProdTopQAPos = maxPosition + 2;
            ImdbTopPos = maxPosition + 2;
            ApfTopPos = maxPosition + 2;
            GSTopPos = maxPosition + 2;
        }

        public bool IsTruth = false;

        public int webPos = 1000;
        public int qaPos = 1000;
        public bool hasQAFact = false;
        public int QAScore = 0;
        public bool hasWebAns = false;
        public int webOcc = 0;
        public int qaOcc = 0;
        public int MaxBM25QAPattern = 0;
        public int MaxBM25Entity = 0;
        public int MaxDRScore = 0;

        // more features
        // 1. prod path
        public int ProdTopWebPos = 1000;
        public double ProdTopWebL2Score = 0;
        public bool ProdHasWeb = false;
        public int ProdWebOcc = 0;
        public int ProdTopQAPos = 1000;
        public double ProdTopQAL2Score = 0;
        public bool ProdHasQAFact = false;
        public int ProdQAOcc = 0;

        //   more.
        public double ProdWebScore = 0;
        public double ProdQAScore = 0;

        // 2. Imdb path
        public int ImdbTopPos = 1000;
        public double ImdbTopL2Score = 0;
        public int ImdbOcc = 0;

        //   more.
        public double ImdbScore = 0;

        // 3. Apf path
        public int ApfTopPos = 1000;
        public double ApfTopL2Score = 0;
        public int ApfOcc = 0;

        //   more.
        public double ApfScore = 0;

        // 4. Graph Search path
        public int GSTopPos = 100;
        public double GSTopL2Score = 0;
        public int GSOcc = 0;

        //   more.
        public double GSScore = 0;

        public static string Header
        { get { return "HasWebAns\tHasQAFact\tWebOcc\tWebPos\tQAOcc\tQAScore\tQAPos\tMaxBM25QAPattern\tMaxBM25Entity\tMaxDRScore"; } }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
                            hasWebAns ? 1 : 0, hasQAFact ? 1 : 0,
                            webOcc, webPos,
                            qaOcc, QAScore, qaPos, 
                            MaxBM25QAPattern, MaxBM25Entity, MaxDRScore);
        }

        public static string LongHeader
        {
            get
            {
                return "ProdTopWebPos\tProdTopWebL2Score\tProdWebOcc\tProdWebScore\tProdHasQAFact\tProdTopQAPos\tProdTopQAL2Score\tProdQAOcc\tProdQAScore\tImdbTopPos\tImdbTopL2Score\tImdbOcc\tImdbScore\tApfTopPos\tApfTopL2Score\tApfOcc\tApfScore\tGSTopPos\tGSTopL2Score\tGSScore";
            }
        }

        public string ToLongString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}",
                            ProdTopWebPos, ProdTopWebL2Score, ProdWebOcc, ProdWebScore,
                            ProdHasQAFact ? 1 : 0, ProdTopQAPos, ProdTopQAL2Score, ProdQAOcc, ProdQAScore,
                            ImdbTopPos, ImdbTopL2Score, ImdbOcc, ImdbScore,
                            ApfTopPos, ApfTopL2Score, ApfOcc, ApfScore,
                            GSTopPos, GSTopL2Score, GSScore);
        }

        public static MovieCandidateFeature FromLongString(string[] items, int start)
        {
            if (items.Length < start + 15)
                return null;
            MovieCandidateFeature fea = new MovieCandidateFeature
            {
                ProdTopWebPos = int.Parse(items[start]),
                ProdTopWebL2Score = double.Parse(items[start + 1]),
                ProdWebOcc = int.Parse(items[start + 2]),
                ProdWebScore = double.Parse(items[start + 3]),
                ProdHasQAFact = int.Parse(items[start + 4]) > 0,
                ProdTopQAPos = int.Parse(items[start + 5]),
                ProdTopQAL2Score = double.Parse(items[start + 6]),
                ProdQAOcc = int.Parse(items[start + 7]),
                ProdQAScore = double.Parse(items[start + 8]),
                ImdbTopPos = int.Parse(items[start + 9]),
                ImdbTopL2Score = double.Parse(items[start + 10]),
                ImdbOcc = int.Parse(items[start + 11]),
                ImdbScore = double.Parse(items[start + 12]),
                ApfTopPos = int.Parse(items[start + 13]),
                ApfTopL2Score = double.Parse(items[start + 14]),
                ApfOcc = int.Parse(items[start + 15]),
                ApfScore = double.Parse(items[start + 16]),
                GSTopPos = int.Parse(items[start + 17]),
                GSTopL2Score = double.Parse(items[start + 18]),
                GSScore = double.Parse(items[start + 19]),
            };
            return fea;
        }

        public static MovieCandidateFeature FromString(string[] items, int start)
        {
            if (items.Length < start + 10)
                return null;

            MovieCandidateFeature fea = new MovieCandidateFeature();
            fea.hasWebAns = int.Parse(items[start]) == 0 ? false : true;
            fea.hasQAFact = int.Parse(items[start + 1]) == 0 ? false : true;
            fea.webOcc = int.Parse(items[start + 2]);
            fea.webPos = int.Parse(items[start + 3]);
            fea.qaOcc = int.Parse(items[start + 4]);
            fea.QAScore = int.Parse(items[start + 5]);
            fea.qaPos = int.Parse(items[start + 6]);
            fea.MaxBM25QAPattern = int.Parse(items[start + 7]);
            fea.MaxBM25Entity = int.Parse(items[start + 8]);
            fea.MaxDRScore = int.Parse(items[start + 9]);

            return fea;
        }

        public double Scoring()
        {
            //if (ProdTopWebPos > 10 && ImdbTopPos > 3 && ApfTopPos > 3 && GSTopPos > 3)
                //return -100;
            //double treeOutput0 = ((ProdTopWebL2Score > 84.4709575) ? ((GSTopPos > 2.5) ? ((ProdTopWebL2Score > 98.270216) ? 0.862348178137658 : ((ImdbOcc > 1.5) ? 0.76470588235294 : ((ProdTopWebL2Score > 86.7813055) ? 0.083743842364532 : -0.999999999999999))) : ((ApfTopPos > 2.5) ? 0.111111111111192 : 0.932543299908842)) : ((GSTopL2Score > 88.678883552555) ? ((GSTopPos > 1.5) ? ((ImdbTopPos > 16.5) ? -0.638554216867469 : 0.0599078341013832) : ((ImdbTopPos > 10.5) ? ((ApfTopPos > 10.5) ? 0.304347826086956 : -1) : 0.666666666666672)) : ((ImdbOcc > 1.5) ? ((ApfTopPos > 2.5) ? -0.999999999999999 : 0.839080459770114) : ((ImdbTopPos > 1.5) ? ((ProdTopWebPos > 7.5) ? ((ApfOcc > 1.5) ? -0.365079365079221 : -0.822380106571955) : ((ProdTopWebL2Score > 76.638853) ? ((ProdTopQAL2Score > 95.2867495) ? 1 : -0.999999999999999) : 0.111111111111111)) : ((ApfOcc > 2.5) ? 0.73913043478261 : -0.341563786008231)))));
            //double treeOutput1 = ((ProdTopWebL2Score > 71.187375) ? ((GSTopL2Score > 97.7884273529) ? 0.715983553710278 : ((ImdbTopPos > 1.5) ? ((ApfTopL2Score > 85.8506175) ? 0.152328842654423 : ((GSTopL2Score > 76.535828828815) ? ((GSTopL2Score > 89.953241825085) ? -1.02857988167881 : 0.453849533476152) : ((ProdTopQAL2Score > 98.8272035) ? 0.835160023017743 : ((ProdTopWebL2Score > 76.638853) ? -0.862356208607727 : -0.103472366510079)))) : ((ProdTopWebL2Score > 77.391134) ? 0.593774862912552 : -0.977037825896976))) : ((GSTopL2Score > 95.62496852875) ? ((ImdbTopL2Score > 95.146231) ? ((GSTopL2Score > 102.90823936465) ? 0.38934188228304 : ((ImdbTopPos > 2.5) ? -1.02325708060213 : 0.274646110630552)) : ((ProdHasQAFact) ? 0.790300623572064 : ((ApfTopL2Score > 73.9370195) ? -0.970174425539226 : ((GSTopPos > 1.5) ? -0.511714301974073 : 0.361542547602187)))) : ((ImdbTopPos > 1.5) ? -0.618019072516995 : ((ApfOcc > 2.5) ? 0.680975723357789 : ((ImdbTopL2Score > 114.9124275) ? 0.574318983968847 : -0.380871077055528)))));
            //double treeOutput2 = ((ProdTopWebL2Score > 86.7813055) ? ((ImdbTopPos > 2.5) ? ((GSTopL2Score > 112.5949077606) ? 0.607843589472513 : -0.0180314492082457) : ((ApfTopPos > 2.5) ? ((ProdTopWebL2Score > 89.9830755) ? -1.19049621687333 : 0.677511567809083) : 0.6723335607621)) : ((GSTopPos > 1.5) ? ((GSTopL2Score > 81.07252919674) ? ((ApfTopPos > 9.5) ? ((GSTopL2Score > 81.09094572067) ? -0.360621994873172 : 1.19226697309164) : ((ProdTopWebPos > 6.5) ? ((ProdTopQAPos > 13.5) ? ((ApfTopL2Score > 73.8472585) ? ((ImdbTopL2Score > 97.837408) ? ((ImdbTopL2Score > 115.835459) ? -1.11110713281557 : 0.195880271466584) : ((ApfTopL2Score > 88.1596145) ? 0.989569710440665 : -0.866426742310515)) : 0.805290758588742) : 0.618463521710356) : -1.02793760142413)) : ((ImdbOcc > 1.5) ? ((ApfTopPos > 2.5) ? -0.773810981604608 : 0.603369332100562) : -0.493061850783684)) : ((ImdbTopPos > 6.5) ? ((GSTopL2Score > 114.45651054385) ? 0.493145582028389 : -0.300565397630865) : 0.469851946266215)));
            //double treeOutput3 = ((ProdTopWebL2Score > 86.7813055) ? ((GSTopPos > 2.5) ? ((ProdTopWebL2Score > 99.5481085) ? 0.594421083613826 : 0.0887306880022481) : 0.61234620679326) : ((GSTopL2Score > 96.36249732975) ? ((GSTopPos > 1.5) ? ((GSTopPos > 3.5) ? -0.949334857995527 : ((ImdbTopPos > 16.5) ? ((GSTopL2Score > 96.9081668854) ? -0.567047597627589 : 1.05032850761211) : ((ProdTopQAPos > 13.5) ? ((GSTopL2Score > 100.8272819519) ? ((ImdbTopL2Score > 114.137762) ? -0.963670205981151 : 0.174188160659449) : -0.927321716061158) : 0.548452302289839))) : 0.376097265229968) : ((ImdbOcc > 1.5) ? ((ApfTopPos > 2.5) ? -0.719706851339878 : 0.540180173764953) : ((ImdbTopPos > 1.5) ? -0.43927440762391 : ((ImdbTopL2Score > 83.747183) ? ((ProdTopWebL2Score > 81.5485335) ? -0.920170985846568 : ((ApfTopL2Score > 81.614284) ? 0.372558775108366 : ((ImdbTopL2Score > 88.011316) ? ((ApfOcc > 2.5) ? 0.756154814676722 : -0.455321004241863) : 0.509004763658224))) : -0.85092407226939)))));
            //double treeOutput4 = ((ProdTopWebL2Score > 74.0203305) ? ((ProdTopWebL2Score > 89.2850835) ? 0.495130462251554 : ((ImdbTopPos > 5.5) ? ((ProdTopWebL2Score > 74.1499815) ? ((ProdTopWebPos > 1.5) ? -0.805683436733638 : 0.21752956060371) : 1.2237797286835) : ((ImdbTopL2Score > 113.39131) ? -1.22911805269157 : 0.259208040833294))) : ((GSTopL2Score > 90.49199914935) ? ((ImdbTopL2Score > 95.215122) ? 0.181023408241327 : ((ImdbTopL2Score > 90.2727465) ? -0.873913797487021 : ((GSTopL2Score > 90.6275925636) ? ((ProdTopQAL2Score > 86.6756205) ? 0.588362934282136 : ((ApfTopL2Score > 73.9370195) ? -0.765310059333768 : ((ApfTopL2Score > 73.7007475) ? 0.823278650498695 : -0.130984439991037))) : 1.16322831210044))) : ((ApfOcc > 1.5) ? ((ApfTopL2Score > 65.518227) ? ((ImdbTopL2Score > 95.146231) ? ((ImdbTopL2Score > 100.637272) ? ((ImdbTopPos > 1.5) ? -0.778238265543595 : 0.323492935859793) : 0.796508340373334) : ((ApfTopPos > 1.5) ? -0.758824440988089 : 0.593045231051197)) : 1.52025312214689) : -0.423003518743813)));
            //double treeOutput5 = ((ProdTopWebL2Score > 71.187375) ? ((GSTopPos > 2.5) ? ((ProdTopWebL2Score > 98.270216) ? 0.457463752807611 : ((ImdbOcc > 1.5) ? 0.473878992455287 : ((ImdbTopL2Score > 104.165864) ? ((ApfTopL2Score > 89.7592055) ? ((ProdTopWebL2Score > 94.7549645) ? 0.154732772020277 : -1.32310693472561) : ((GSTopL2Score > 89.953241825085) ? -0.977894471416641 : 0.430471945506647)) : ((ImdbTopL2Score > 98.4311725) ? -1.02232836817698 : ((GSTopL2Score > 74.053573131565) ? 0.421325862743051 : ((ProdTopQAL2Score > 98.8272035) ? 0.814126375990215 : -0.413455592878756)))))) : 0.459100227249479) : ((GSTopL2Score > 100.8272819519) ? ((ImdbTopL2Score > 95.146231) ? ((ApfTopL2Score > 80.6866845) ? ((ApfTopPos > 6.5) ? -1.09575870670193 : ((ImdbTopPos > 1.5) ? 0.277317823516778 : -1.36070425352016)) : 0.405979795204149) : ((GSTopL2Score > 103.4546966553) ? -0.435480761406509 : ((ApfTopL2Score > 29.688919) ? -0.760800740483597 : 0.56779228096949))) : ((ApfOcc > 1.5) ? 0.0113944079619062 : -0.334014149338778)));
            //double treeOutput6 = ((ProdTopWebL2Score > 89.2850835) ? 0.407534986253997 : ((GSTopPos > 1.5) ? ((ProdTopWebL2Score > 88.2868445) ? -1.02288983931348 : ((ProdTopQAL2Score > 99.334494) ? 0.530486701194712 : ((ProdTopWebL2Score > 87.978338) ? 0.568454687112443 : ((GSTopL2Score > 69.89696598055) ? ((GSTopL2Score > 69.9166841507) ? -0.100279551766704 : 2.24849262860254) : ((ImdbTopPos > 2.5) ? -0.413549525140465 : ((ImdbTopL2Score > 104.985542) ? ((ApfTopL2Score > 91.496494) ? 0.360173360221324 : -0.757528465684519) : ((ImdbTopL2Score > 99.99526) ? 0.465281130219465 : ((ImdbOcc > 1.5) ? 0.445222303296302 : ((ImdbTopL2Score > 87.4845165) ? ((ImdbTopL2Score > 88.509242) ? ((ApfTopPos > 19.5) ? 0.140611118187371 : -0.534963340380961) : 1.38369937675515) : -0.681630439663417))))))))) : ((GSTopL2Score > 93.4354186058) ? 0.278064565285588 : ((ApfTopPos > 1.5) ? ((GSTopL2Score > 54.84314155575) ? -0.757846170622396 : ((GSTopL2Score > 54.69058418275) ? 2.11891388074327 : -0.660767140874555)) : 0.384029028419507))));
            //double treeOutput7 = ((ProdTopWebL2Score > 71.187375) ? ((ImdbOcc > 1.5) ? 0.481543350759801 : ((GSTopL2Score > 97.7884273529) ? 0.287897388437432 : ((ApfTopL2Score > 100.0817295) ? 0.468331570395497 : ((ProdTopWebL2Score > 77.5149775) ? ((ImdbTopL2Score > 103.995258) ? ((ApfTopL2Score > 89.7592055) ? ((ApfTopPos > 1.5) ? -1.12400722649238 : -0.049742216751787) : 0.247359369054834) : ((ProdTopQAPos > 4.5) ? -0.575231410445297 : 0.484211320268202)) : ((ApfOcc > 1.5) ? -0.826934171551449 : 0.312598381345432))))) : ((GSTopL2Score > 90.49199914935) ? ((ImdbTopL2Score > 105.5515175) ? 0.188389394323508 : ((ImdbTopL2Score > 101.905514) ? ((GSTopL2Score > 100.95313453675) ? -1.22378510577628 : ((GSTopL2Score > 100.26166152955) ? 0.887870140257241 : -0.793338614674715)) : ((ApfTopL2Score > 85.2706065) ? 0.378150177825394 : -0.0879061600363743))) : ((ApfOcc > 1.5) ? ((ApfTopL2Score > 65.518227) ? ((ApfTopPos > 2.5) ? -0.229361929962016 : 0.419050416111922) : 1.07551128852062) : -0.304156931145117)));
            //double treeOutput8 = ((GSTopPos > 1.5) ? ((ProdTopWebPos > 1.5) ? ((ApfOcc > 2.5) ? ((ImdbTopPos > 1.5) ? ((ApfTopL2Score > 87.6277625) ? 0.198500674180407 : -0.774395578374067) : 0.488980066244794) : ((GSTopL2Score > 113.59338378905) ? 0.09286611795001 : ((ProdTopWebL2Score > 92.845945) ? -1.30989134402837 : ((GSTopL2Score > 109.60061073305) ? -0.957210143883747 : ((ProdTopWebL2Score > 92.7276305) ? 0.957743027271047 : ((ImdbTopPos > 4.5) ? ((GSTopL2Score > 69.89696598055) ? ((ImdbTopPos > 8.5) ? ((GSTopL2Score > 69.9166841507) ? ((ImdbTopPos > 12.5) ? -0.133125021796042 : ((ApfTopL2Score > 75.530588) ? -0.698303770696435 : 0.706491142853445)) : 1.30333761017433) : -0.753641860702942) : -0.514554793124825) : ((ImdbTopL2Score > 73.1525725) ? -0.104308115709355 : 1.19413887013685))))))) : 0.238431410062707) : ((GSTopL2Score > 96.36249732975) ? 0.345021323826918 : ((ImdbTopPos > 2.5) ? ((GSTopL2Score > 54.84314155575) ? -0.683777450200923 : 0.619043558193031) : 0.256583847646947)));
            //double treeOutput9 = ((ProdTopWebL2Score > 89.2850835) ? 0.312065985120928 : ((ProdTopQAPos > 7.5) ? ((ApfTopL2Score > 94.953098) ? -0.954601372142696 : ((ImdbTopL2Score > 95.1916525) ? ((GSTopL2Score > 100.8272819519) ? 0.152278019646406 : ((GSTopL2Score > 97.866396904) ? -0.921320285282697 : -0.0605812366397878)) : ((ImdbTopL2Score > 88.509242) ? -0.412405037289565 : ((ImdbTopL2Score > 88.3807325) ? 1.03833306879071 : ((ImdbOcc > 1.5) ? 0.484111966757801 : ((ApfTopL2Score > 76.532382) ? -0.544192204371009 : ((ApfTopPos > 13.5) ? ((GSTopL2Score > 106.3937320709) ? -0.80771487873149 : ((GSTopL2Score > 102.1856813431) ? ((GSTopL2Score > 102.23370170595) ? 0.134409858070406 : 1.38488279884263) : -0.221751795610573)) : ((GSTopL2Score > 78.20370388031) ? 0.557677181249657 : ((ImdbTopL2Score > 87.4845165) ? 0.780421183412436 : ((ProdTopWebL2Score > 76.3140495) ? 1.17661766344686 : ((ApfTopL2Score > 67.993887) ? -0.470926636918044 : ((ApfTopL2Score > 67.6711805) ? 1.79000149560698 : -0.609537793314751)))))))))))) : 0.288238543151198));
            //double output = treeOutput0 + treeOutput1 + treeOutput2 + treeOutput3 + treeOutput4 + treeOutput5 + treeOutput6 + treeOutput7 + treeOutput8 + treeOutput9;
            
            //double treeOutput0 = ((ProdTopWebL2Score > 84.4709575) ? ((GSTopPos > 2.5) ? ((ProdTopWebL2Score > 98.270216) ? 0.862348178137658 : ((ImdbOcc > 1.5) ? 0.76470588235294 : ((ProdTopWebL2Score > 86.7813055) ? 0.083743842364532 : -0.999999999999999))) : ((ApfTopPos > 2.5) ? 0.111111111111192 : 0.932543299908842)) : ((GSTopL2Score > 88.678883552555) ? ((GSTopPos > 1.5) ? ((ImdbTopPos > 16.5) ? -0.638554216867469 : 0.0599078341013832) : ((ImdbTopPos > 10.5) ? ((ApfTopPos > 10.5) ? 0.304347826086956 : -1) : 0.666666666666672)) : ((ImdbOcc > 1.5) ? ((ApfTopPos > 2.5) ? -0.999999999999999 : 0.839080459770114) : ((ImdbTopPos > 1.5) ? ((ProdTopWebPos > 7.5) ? ((ApfOcc > 1.5) ? -0.365079365079221 : -0.822380106571955) : ((ProdTopWebL2Score > 76.638853) ? ((ProdTopQAL2Score > 95.2867495) ? 1 : -0.999999999999999) : 0.111111111111111)) : ((ApfOcc > 2.5) ? 0.73913043478261 : -0.341563786008231)))));
            //double output = treeOutput0;

            // LR with training data
            //double output = -1.40351188851018 * ProdTopWebPos + 2.14196354063053 * ProdWebOcc + 0.549239194427028 * (ProdHasQAFact ? 1 : 0) + -0.658262534064392 * ProdTopQAPos + 0.830753698987367 * ProdQAOcc + -0.851282296356059 * ImdbTopPos + 2.07706957784902 * ImdbOcc + 0.0520139211072109 * ApfTopPos + 2.41491463108738 * ApfOcc + -1.57002253671179 * GSTopPos + 1.5962481243113;

            // LR with 5k data
            //double output = -0.219875482164836 * ProdTopWebPos + 5.11513880332912 * ProdWebOcc + -0.0190905754775647 * (ProdHasQAFact ? 1  : 0) + -0.5667417622592 * ProdTopQAPos + 2.74317463617034 * ProdQAOcc + -0.764273746625126 * ImdbTopPos + 2.86109921047639 * ImdbOcc + -0.120453259259041 * ApfTopPos + 1.65556621000396 * ApfOcc + -1.52913028025035 * GSTopPos + 0.300585820289968;

            // LR with new feature
            // 20150409 version
            //double output = 2.889235 * ProdWebScore + 4.173802 * ProdQAScore + 3.812449 * ImdbScore + 3.520511 * GSScore + -2.279276;
            
            // 20150410 version
            //double output = ProdWebScore + 2.4 * ProdQAScore + 1.2 * ImdbScore + 0.6 * ApfScore + 1.2 * GSScore;

            // Only-2-param version
            double output = ProdWebScore + 1.1 * ImdbScore + 1.8 * ApfScore;

            // LR with 70% training data, dup 5
            //double output = -1.497078 * ProdTopWebPos + 1.732595 * ProdWebOcc + 0.6587626 * (ProdHasQAFact ? 1 : 0) + -1.075198 * ProdTopQAPos + 0.6884053 * ProdQAOcc + -0.2948771 * ImdbTopPos + 2.128939 * ImdbOcc + -0.2396228 * ApfTopPos + 1.833391 * ApfOcc + -0.8720151 * GSTopPos + 1.360125;

            // Decision tree with 5k data
            //double output = ((ApfOcc > 1.5) ? ((ImdbTopPos > 1.5) ? ((GSTopPos > 1.5) ? ((ProdWebOcc > 1.5) ? 0.578947368421063 : 0.12315270935961) : 0.711392405063299) : 0.900172117039743) : ((GSTopPos > 1.5) ? ((ProdTopWebPos > 21) ? ((ImdbOcc > 1.5) ? ((ProdTopQAPos > 21) ? ((ImdbOcc > 2.5) ? 0.363636363636365 : -0.240915208613732) : 0.5) : ((ImdbTopPos > 16.5) ? -0.752385865360026 : ((GSTopPos > 4.5) ? -0.710057384476079 : ((ApfTopPos > 12.5) ? -0.47195858498707 : -0.175693527080584)))) : ((ImdbOcc > 1.5) ? 0.266055045871551 : ((ProdWebOcc > 1.5) ? 0.538461538461569 : ((GSTopPos > 13.5) ? ((ImdbTopPos > 1.5) ? -0.511543134872414 : -0.219138755980865) : 0.017801047120419)))) : ((ProdTopWebPos > 19.5) ? ((ImdbTopPos > 9.5) ? ((ProdTopQAPos > 16.5) ? -0.249011857707516 : 0.588235294117641) : 0.394052044609667) : 0.716563330380846)));

            // LR with 33k data
            //double output = -0.436750017900487 * ProdTopWebPos + 1.57275548464789 * ProdWebOcc + 0.0372825748246007 * (ProdHasQAFact ? 1 : 0) + -1.14769773907408 * ProdTopQAPos + 2.4875707952285 * ProdQAOcc + 4.56890644244016 * ImdbOcc + -0.295033871519273 * ApfTopPos + 1.85430832261554 * ApfOcc + 0.182394683941752;

            // SVM with 33k data
            //double output = -1.21604601492156 * ProdTopWebPos + 0.839454966100061 * ProdWebOcc + 0.382637960659113 * (ProdHasQAFact ? 1 : 0) + -0.628120096415523 * ProdTopQAPos + 0.324817196298178 * ProdQAOcc + -0.912635118854355 * ImdbTopPos + 2.9939604775272 * ImdbOcc + 0.0612218059385622 * ApfTopPos + 1.17495418822106 * ApfOcc + 0.477606004137611;
            return output;
        }
    }

    public enum MoviePath
    {
        Prod,
        Imdb,
        Apf,
        GS
    }
}
