using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScopeRuntime;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

public class Misc
{
    public static string GetID(string id)
    {
        if (id.StartsWith("http://knowledge.microsoft.com/"))
        {
            return id.Substring("http://knowledge.microsoft.com/".Length);
        }
        else
            return "";
    }

    public static int GroupId(string p, int N)
    {
        return int.Parse(p.Substring(1, p.IndexOf('.', 1) - 1)) % N;
    }

    public static bool ContainPropertyTerm(string s, string p)
    {
        string term = p.Split('.').Last();

        string[] terms = term.Split('_');
        HashSet<string> set = new HashSet<string>();
        foreach (var t in terms)
        {
            set.Add(t);
            if (t.Last() == 's')
            {
                set.Add(t.Substring(0, t.Length - 1));
            }
            else
            {
                set.Add(t + "s");
            }
        }

        string[] sTerms = s.Split(' ');
        foreach (var t in sTerms)
        {
            if (set.Contains(t))
                return true;
        }

        return false;
    }

    public static string GetType(string str)
    {
        str = str.ToLower();
        if (string.IsNullOrEmpty(str) || !str.StartsWith("http://knowledge.microsoft.com/mso/"))
            return string.Empty;

        int start = "http://knowledge.microsoft.com/mso/".Length;
        str = str.Substring(start, str.Length - start);
        return string.Join(".", str.Split('.').Take(2));
    }

    public static HashSet<string> Stopwords = new HashSet<string>(new string[] { "who", "cortana", "i want to know", "tell me", "show me", "corta", "do you know", "of", "is", "and", "the", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "an", "are", "am", "were", "was", "http", "www", "https", "com", "org", "be", "but", "or", "that", "this", "will" });

    public static string OrderFreePattern(string p)
    {
        string[] arr = p.Split(' ');
        //var v = arr.Where(a => !Stopwords.Contains(a)).ToArray();
        Array.Sort(arr);
        return string.Join(" ", arr);
    }

    public static string GenPattern(string src, string term, string pattern)
    {
        if (!string.IsNullOrEmpty(pattern))
        {
            return pattern;
        }
        else if (string.IsNullOrWhiteSpace(term))
        {
            return "";
        }
        else if (term.Contains("<e>"))
        {
            return pattern.Replace("<e>", src);
        }
        else
        {
            return src + " " + term;
        }
    }

    public static Regex regex = new Regex(@"\b(who|cortana|i want to know|tell me|show me|corta|do you know|of|is|and|the|a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z|an|are|am|were|was|http|www|https|com|org|be|but|or|that|this|will)\b");
    public static string ReplaceStopword(string p)
    {
        p = regex.Replace(p, "XSTOPWORD");
        return p;
    }

    public static Tuple<string, string> GetPatternAndQueryToken(string p, string q)
    {
        try
        {
            p = p.Replace('.', '+');
            List<string> anchors = new List<string>();
            List<string> newP = new List<string>();
            foreach (var s in p.Split(' '))
            {
                if (!s.Contains('+'))
                {
                    newP.Add(s);
                }
                else
                {
                    newP.Add(string.Format("(?<m{0}>.+?)", anchors.Count));
                    anchors.Add(s.Replace('+', '.'));
                }
            }

            string regex = "^" + string.Join(" ", newP) + "$";
            var m = Regex.Match(q, regex);
            if (m == null || !m.Success)
            {
                return new Tuple<string, string>("", "");
            }

            List<string> tokens = new List<string>(anchors.Count);
            for (int i = 0; i < anchors.Count; i++)
            {
                tokens.Add(m.Groups[string.Format("m{0}", i)].Value);
            }

            return new Tuple<string, string>(string.Join(",", anchors), string.Join(",", tokens));
        }
        catch { return new Tuple<string, string>("", ""); }
    }

    static HashSet<string> dualEntityTypes = new HashSet<string>("american_football.coach_position;amusement_parks.ride_type;architecture.style;architecture.type_of_museum;astronomy.celestial_object_category;aviation.accident_type;aviation.aircraft_engine_type;aviation.aircraft_type;award.category;award.competition_type;award.hall_of_fame_induction_category;baseball.coaching_position;baseball.position;boats.ship_type;book.literary_genre;book.magazine_genre;book.periodical_subject;book.subject;broadcast.genre;business.board_member_title;business.company_type;business.issue_type;business.job_title;business.product_category;comic_books.genre;commerce.digital_camera_storage_type;computer.file_format_genre;computer.software_genre;cvg.computer_game_subject;cvg.genre;education.academic_post_title;education.school_type;fictional_universe.job_title;film.company_role;film.genre;film.job_role;film.subject;food.beer_style;food.type_of_dish;food.wine_style;games.genre;geography.geographical_feature_category;geography.lake_type;geography.mountain_type;government.appointed_role;government.legislative_committee_title;government.office_category;government.office_or_title;internet.website_category;law.crime_type;law.judicial_title;law.legal_subject;media_common.media_genre;media_common.quotation_subject;metropolitan_transit.transit_service_type;military.rank;music.genre;music.music_video_genre;music.performance_role;organization.committee_title;organization.contact_category;organization.role;organization.type;people.profession;people.professional_field;projects.project_role;protected_sites.site_listing_category;religion.religious_leadership_title;royalty.noble_rank;royalty.noble_title;sports.award_type;sports.coaching_position;sports.sports_position;theater.designer_role;theater.genre;theater.production_staff_role;transportation.bridge_type;tv.crew_role;tv.genre;tv.non_character_role;tv.producer_type;tv.subject;visual_art.art_subject;visual_art.visual_art_genre".Split(';'));
    static HashSet<string> intentDualEntityTypes = new HashSet<string>("american_football.coach_position;baseball.coaching_position;baseball.position;business.board_member_title;business.job_title;education.academic_post_title;fictional_universe.job_title;film.company_role;film.job_role;government.appointed_role;government.legislative_committee_title;government.office_or_title;law.judicial_title;military.rank;music.performance_role;organization.committee_title;organization.role;projects.project_role;religion.religious_leadership_title;royalty.noble_rank;royalty.noble_title;sports.award_type;sports.coaching_position;sports.sports_position;theater.designer_role;theater.production_staff_role;tv.crew_role;tv.non_character_role".Split(';'));
    public static bool IsDualEntityType(string type)
    {
        return dualEntityTypes.Contains(type);
    }

    public static bool ContainIntentWord(string pattern, string dual1, string dual2)
    {
        int cntResidual = pattern.Split(' ').Where(t => !t.Contains('.')).Count();
        if (cntResidual == 0)
        {
            if (intentDualEntityTypes.Contains(dual1) || intentDualEntityTypes.Contains(dual2))
                return true;
            else
                return false;
        }
        else
            return true;
    }

    public static string ReplaceIntent(string p) 
    { 
        p = Regex.Replace(p, @"\b(the good wife)\b", "B_Entity"); 
        p = Regex.Replace(p, @"\b(networth|net worth|worth)\b", "IFact_Networth"); 
        p = Regex.Replace(p, @"\b(book by|book about|book|biography|bio)\b", "IFact_Book"); 
        p = Regex.Replace(p, @"\b(die|dies|died|dead|cause of death|date of death|death)\b", "IFact_Death"); 
        p = Regex.Replace(p, @"\b(age|how old)\b", "IFact_Age");
        p = Regex.Replace(p, @"\b(child|children|kids|kid|son|daughter|sister|brother|mother|father|parent|friend|band)s?\b", "IFact_RelatedEntities");
        p = Regex.Replace(p, @"\b(nationality|country)\b", "IFact_Nationality");
        p = Regex.Replace(p, @"\b(ethnicity|race)\b", "IFact_Ethnicity");
        p = Regex.Replace(p, @"\b(how tall|height)\b", "IFact_Height");
        p = Regex.Replace(p, @"\b(pic|picture|image|photo|look like|looks like|bikini)(s|es)?\b", "IFact_Pic");
        p = Regex.Replace(p, @"\b(video|porn|movie|tv show)(s|es)?\b", "IFact_Video");
        p = Regex.Replace(p, @"\b(song|music)(s|es)?\b", "IFact_Song");
        p = Regex.Replace(p, @"\bhow (many|much)\b", "IFact_Quantity");
        p = Regex.Replace(p, @"\b(how long|duration)\b", "IFact_Duration");
        p = Regex.Replace(p, @"\b(when|what year|what time|birthdate|birth date|date|year)\b", "IFact_TimeRelated");
        p = Regex.Replace(p, @"\b(profession|job|living|occupation)\b|\bwhat (does|did) .* do\b", "IFact_Job");
        p = Regex.Replace(p, @"\b(facebook|twitter|tmz|instgram|instagram|imdb|playboy|play boy|bbc|tumblr)\b", "IFact_Nav");
        p = Regex.Replace(p, @"\b(story|quote|quotes|records|award|where|religion|education|career|health|fact|secret|rumor|tatoo)s?\b", "IFact_OtherFact");
        p = Regex.Replace(p, @"\b(happen|arrest|divorc|seperat|separat|split|news|accident|obituary|controversy|scandal|interview|cheat|unfaithful|cuckold|gay|alcoholic|alive|living)(e|ing|s|es|d|ed)?\b", "IEvent_All");
        p = Regex.Replace(p, @"\b(kill|fight|abuse|abusive|cancer|meet|met|sex|fuck|bang|beat|beater|hit|swap|suicide|murder|shoot|shot|break up|leav|left|kiss|catch|caught|funeral|pregnant|affair)(e|ing|s|es|d|ed)?\b", "IEvent_All"); 
        p = Regex.Replace(p, @"\b(crossword|cross word)\b", "Other_Crossword");
        p = Regex.Replace(p, @"\b(ass|cock|dick|boob|suck|threesome|prenup|pussy|bitch|nude|naked|slut|gay|lesbian|founderscard)s?\b", "Other_Others");

        p = Regex.Replace(p, @"\b(comedian|actor|actress|director|singer|musician|drummer|artist|gov|governor|president|congressman|congress man|politician|sniper|preacher|pastor|doctor|dr|captain|military|lawyer|judge|chef|rapper|amateur)\b", "Cons_Prefession");
        p = Regex.Replace(p, @"\b(player|nfl|nba|hockey|basketball|golfer|golf|baseball|football|soccer|coach|espn)( player)?\b", "Cons_Prefession");
        p = Regex.Replace(p, @"\b(white|black|brown|first|1st|second|2nd|third|3rd|fourth|4th|last|latest|ex|former|previous|past|deceased|current|new|main|now|present|recent|today|old|young|missing)\b", "Cons_Meaningful");
        p = Regex.Replace(p, @"\b(american|mexican|chinese|japanese|asian|indian|filipina|italian|foreign|nigerian|pakistani|hispanic|jewish)\b", "Cons_Nationality");
        p = Regex.Replace(p, @"\b(mormon|christian|catholic)\b", "Cons_Religion");
        p = Regex.Replace(p, @"\bin (the )?(jail|prison)\b|\b(hollywood)\b", "Cons_Place");
        p = Regex.Replace(p, @"\b(\d{4})\b", "Cons_Year");
        p = Regex.Replace(p, @"\bin (the )?(bible|movie|show|real life)\b|\b(bible)\b", "Cons_Origin");
        p = Regex.Replace(p, @"\b(real|mature|crazyfat|favorite|sexy|hot|rich|perfect|beautiful|lovely|beloved|pretty|blonde|redhead|brunette|blond|petite|milf|submissive|plump|disabled)\b", "Cons_Description"); 
        return p; 
    }
}

/// <summary>
///  
/// </summary>
public class LongestMatchReducer : Reducer
{
    class Info
    {
        public int start;
        public int end;
        public int length;
        public string name;
    }

    static string[] ReplaceMatch(string[] str, int beg, int end, string replace)
    {
        string[] replaces = replace.Split(' ');
        if (replaces.Length > end - beg)
        {
            throw new Exception("replacement no more than original");
        }

        string[] replacements = new string[end - beg];
        for (int i = 0; i < replaces.Length; i++)
        {
            replacements[i] = replaces[i];
        }

        for (int i = replaces.Length; i < replacements.Length; i++)
        {
            replacements[i] = "MSMagicWord";
        }

        return (str.Take(beg).Concat(replacements).Concat(str.Skip(end))).ToArray();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        var schema = input.Clone();
        schema.Add(new ColumnInfo("RQ", ColumnDataType.String));
        return schema;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override IEnumerable<Row> Reduce(RowSet input, Row output, string[] args)
    {
        List<Info> matches = new List<Info>();
        int count = 0;
        string q = string.Empty;
        foreach (Row row in input.Rows)
        {
            if (++count == 1)
            {
                q = row["Q"].String;
                output["Q"].Set(q);
            }

            matches.Add(new Info() { start = row["Start"].Integer, end = row["End"].Integer, length = row["End"].Integer - row["Start"].Integer, name = row["Name"].String });
        }

        var sorted = (from m in matches orderby m.length descending, m.start ascending select m).ToList();
        string[] str = q.Split(' ');
        int queryLen = str.Length;
        bool[] hasLabel = new bool[queryLen];
        for (int i = 0; i < queryLen; i++)
        {
            hasLabel[i] = false;
        }

        List<Info> filtered = new List<Info>();
        foreach (var m in sorted)
        {
            if (m.start < 0 || m.end > queryLen)
                continue;

            bool valid = true;
            for (int i = m.start; i < m.end; i++)
            {
                if (hasLabel[i])
                {
                    valid = false;
                    break;
                }
            }

            if (!valid)
                continue;

            for (int i = m.start; i < m.end; i++)
            {
                hasLabel[i] = true;
            }

            filtered.Add(m);
        }

        string[] rq = str;
        foreach (var f in filtered)
        {
            rq = ReplaceMatch(rq, f.start, f.end, args[0]);
        }
        string temp = string.Join(" ", rq);

        foreach (var f in filtered)
        {
            output["Start"].Set(f.start);
            output["End"].Set(f.end);
            output["Name"].Set(f.name);
            output["RQ"].Set(temp);
            yield return output;
        }
    }
}



/// <summary>
/// 
/// </summary>
public class GenXMLProcessor : Processor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return input.Clone();
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>    
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        
        foreach (Row row in input.Rows)
        {
            string xml = row[0].String;
            var matches = Regex.Matches(xml, "\\<.+?\\>");
            foreach (Match m in matches)
            {
                output[0].Set(m.Value);
                yield return output;
            }
        }
    }
}


/// <summary>
/// 
/// </summary>
public class XPathProcessor : Processor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        return new Schema("sid, path");
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>    
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        
        foreach (Row row in input.Rows)
        {
            string xml = row[0].String;
            XDocument xdoc = XDocument.Load(new StringReader(xml));
            var nodes = xdoc.XPathSelectElements("//entity");
            //XmlDocument xdoc = new XmlDocument();
            //xdoc.LoadXml(xml);

            //var nodes = xdoc.SelectNodes("//entity");
            foreach (XElement n in nodes)
            {
                string sid = n.Attribute("sid").Value;
                string path = XExtensions.GetAbsoluteXPath(n);
                output[0].Set(sid);
                output[1].Set(path);
                yield return output;
            }
        }
    }
}


public class XExtensions
{
    /// <summary>
    /// Get the absolute XPath to a given XElement, including the namespace.
    /// (e.g. "/a:people/b:person[6]/c:name[1]/d:last[1]").
    /// </summary>
    public static string GetAbsoluteXPath(XElement element)
    {
        if (element == null)
        {
            throw new ArgumentNullException("element");
        }

        Func<XElement, string> relativeXPath = e =>
        {
            int index = IndexPosition(e);
            string name = e.Name.LocalName;
            //string p = String.Join(" ", name.Split(' ').Select(t => (t.Contains('.') ? "*" : t)));
            string id = "";
            if (name.Equals("property"))
            {
                id = e.Attribute("id").Value;
            }

            // If the element is the root, no index is required
            return (index == -1) ? "/" + name : string.Format("/{0}", name) + (id == "" ? "" : string.Format("[@id=\\\"{0}\\\"]", id));
        };

        var ancestors = from e in element.Ancestors()
                        select relativeXPath(e);

        return string.Concat(ancestors.Reverse().ToArray()) +
               relativeXPath(element);
    }

    /// <summary>
    /// Get the index of the given XElement relative to its
    /// siblings with identical names. If the given element is
    /// the root, -1 is returned.
    /// </summary>
    /// <param name="element">
    /// The element to get the index of.
    /// </param>
    public static int IndexPosition(XElement element)
    {
        if (element == null)
        {
            throw new ArgumentNullException("element");
        }

        if (element.Parent == null)
        {
            return -1;
        }

        int i = 1; // Indexes for nodes start at 1, not 0

        foreach (var sibling in element.Parent.Elements(element.Name))
        {
            if (sibling == element)
            {
                return i;
            }

            i++;
        }

        throw new InvalidOperationException
            ("element has been removed from its parent.");
    }
}


/// <summary>
/// 
/// </summary>
public class QueryGenProcessor : Processor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="args"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override Schema Produces(string[] columns, string[] args, Schema input)
    {
        var output = input.Clone();
        output.Add(new ColumnInfo("Query", ColumnDataType.String));
        return output;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="args"></param>
    /// <returns></returns>    
    public override IEnumerable<Row> Process(RowSet input, Row output, string[] args)
    {
        foreach (Row row in input.Rows)
        {
            row.CopyTo(output);
            output[6].Set("");
            output[7].Set("");
            string srcType = row[0].String;
            string pattern = row[3].String;
            string consType = pattern.Split(' ').Where(t => t.Contains('.') && t != srcType).FirstOrDefault();
            if (string.IsNullOrEmpty(consType))
                continue;

            string srcNames = row[6].String;
            string consNames = row[7].String;
            foreach (var src in srcNames.Split(';'))
            {
                foreach (var cons in consNames.Split(';'))
                {
                    string query = pattern.Replace(srcType, src).Replace(consType, cons);
                    output["Query"].Set(query);
                    yield return output;
                }
            }
            yield return output;
        }
    }
}


