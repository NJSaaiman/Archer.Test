using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test.Helpers
{
    class Randomizer
    {
        private static Random _random = new Random(DateTime.Now.Second);

        public string RandomName
        {
            get
            {
                string name = string.Empty;
                if (_random.Next(1, 2) == 1)
                {
                    name = maleNames[_random.Next(0, maleNames.Length)] + " " + lastNames[_random.Next(0, lastNames.Length)];
                }
                else
                {
                    name = femaleNames[_random.Next(0, femaleNames.Length)] + " " + lastNames[_random.Next(0, lastNames.Length)];
                }
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                return ti.ToTitleCase(name.ToLowerInvariant());
            }

        }


        public string RandomCellNumber
        {
            get
            {
               
                string output = string.Empty;

                output +=string.Concat(_random.Next(100000000, 999999999).ToString().Take(3));
                output += string.Concat(_random.Next(100000000, 999999999).ToString().Take(3));
                output += string.Concat(_random.Next(100000000, 999999999).ToString().Take(4));

                //we need some invalid number lets modify them if they hit the unluck number.
                switch (_random.Next(0, 7))
                {
                    case 0: //make one digit short
                        output = string.Concat(output.Take(9));
                        break;
                    case 3: //make it AlphaNumeric
                        output = string.Concat(output.Take(9)) + "A";
                        break;
                }


                return output;
            }

        }

        public string GetEmailAddress(string name)
        {
            name = name.Replace(" ", ".") + "@" + mailDomains[_random.Next(0, mailDomains.Length)];

            return name;
        }


        public string GetCompanyName()
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            string company = companyNames[_random.Next(0, companyNames.Length)];
            return ti.ToTitleCase(company.ToLowerInvariant());
        }



        private readonly static string[] maleNames = new string[100] { "Michael", "James", "John", "Robert", "David", "William", "Christopher", "Joseph", "Richard", "Thomas", "Daniel", "Matthew", "Charles", "Anthony", "Mark", "Joshua", "Steven", "Andrew", "Brian", "Kevin", "Jason", "Timothy", "Paul", "Kenneth", "Jeffrey", "Ryan", "Donald", "Eric", "Nicholas", "Ronald", "Jacob", "Jonathan", "Justin", "Edward", "Brandon", "Stephen", "Gary", "Scott", "George", "Gregory", "Larry", "Benjamin", "Patrick", "Samuel", "Alexander", "Tyler", "Aaron", "Adam", "Zachary", "Jose", "Dennis", "Nathan", "Kyle", "Jerry", "Douglas", "Raymond", "Frank", "Peter", "Jeremy", "Sean", "Jack", "Keith", "Christian", "Austin", "Bryan", "Terry", "Ethan", "Jordan", "Jesse", "Henry", "Roger", "Dylan", "Juan", "Shawn", "Carl", "Bruce", "Gerald", "Travis", "Randy", "Bradley", "Gabriel", "Alan", "Noah", "Walter", "Lawrence", "Cody", "Craig", "Todd", "Billy", "Logan", "Phillip", "Joe", "Carlos", "Vincent", "Arthur", "Wayne", "Johnny", "Philip", "Chad", "Russell" };
        private readonly static string[] femaleNames = new string[100] { "Mary", "Jennifer", "Patricia", "Linda", "Elizabeth", "Jessica", "Barbara", "Susan", "Lisa", "Sarah", "Karen", "Ashley", "Kimberly", "Michelle", "Amanda", "Melissa", "Nancy", "Emily", "Sandra", "Stephanie", "Rebecca", "Amy", "Donna", "Laura", "Angela", "Deborah", "Cynthia", "Carol", "Margaret", "Sharon", "Nicole", "Kathleen", "Samantha", "Heather", "Brenda", "Pamela", "Betty", "Rachel", "Christine", "Katherine", "Debra", "Julie", "Kelly", "Maria", "Christina", "Anna", "Lauren", "Megan", "Shirley", "Diane", "Dorothy", "Catherine", "Janet", "Carolyn", "Andrea", "Victoria", "Emma", "Cheryl", "Hannah", "Amber", "Danielle", "Teresa", "Jacqueline", "Sara", "Brittany", "Joyce", "Judith", "Kathryn", "Denise", "Tiffany", "Olivia", "Helen", "Madison", "Crystal", "Lori", "Tammy", "Kayla", "Alexis", "Virginia", "Diana", "Janice", "Erin", "Joan", "Ann", "Ruth", "Julia", "Natalie", "Taylor", "Theresa", "Abigail", "Judy", "Shannon", "Martha", "Kathy", "Alyssa", "Gloria", "Tina", "Dawn", "Allison", "Beverly" };
        private readonly static string[] lastNames = new string[147] { "SMITH", "JONES", "BROWN", "JOHNSON", "WILLIAMS", "MILLER", "TAYLOR", "WILSON", "DAVIS", "WHITE", "CLARK", "HALL", "THOMAS", "THOMPSON", "MOORE", "HILL", "WALKER", "ANDERSON", "WRIGHT", "MARTIN", "WOOD", "ALLEN", "ROBINSON", "LEWIS", "SCOTT", "YOUNG", "JACKSON", "ADAMS", "TRYNISKI", "GREEN", "EVANS", "KING", "BAKER", "JOHN", "HARRIS", "ROBERTS", "CAMPBELL", "JAMES", "STEWART", "LEE", "COUNTY", "TURNER", "PARKER", "COOK", "EDWARDS", "MORRIS", "MITCHELL", "BELL", "WARD", "WATSON", "MORGAN", "DAVIES", "COOPER", "PHILLIPS", "ROGERS", "GRAY", "HUGHES", "HARRISON", "CARTER", "MURPHY", "COLLINS", "HENRY", "FOSTER", "RICHARDSON", "RUSSELL", "HAMILTON", "SHAW", "BENNETT", "HOWARD", "REED", "FISHER", "MARSHALL", "MAY", "CHURCH", "WASHINGTON", "KELLY", "PRICE", "MURRAY", "WILLIAM", "PALMER", "STEVENS", "COX", "ROBERTSON", "MISS", "CLARKE", "BAILEY", "GEORGE", "NELSON", "MASON", "BUTLER", "MILLS", "HUNT", "ISLAND", "SIMPSON", "GRAHAM", "HENDERSON", "ROSS", "STONE", "PORTER", "WALLACE", "KENNEDY", "GIBSON", "WEST", "BROOKS", "ELLIS", "BARNES", "JOHNSTON", "SULLIVAN", "WELLS", "HART", "FORD", "REYNOLDS", "ALEXANDER", "COLE", "FOX", "HOLMES", "DAY", "CHAPMAN", "POWELL", "WEBSTER", "LONG", "RICHARDS", "GRANT", "HUNTER", "WEBB", "THOMSON", "LINCOLN", "GORDON", "WHEELER", "STREET", "PERRY", "BLACK", "LANE", "GARDNER", "CITY", "LAWRENCE", "ANDREWS", "WARREN", "SPENCER", "RICE", "JENKINS", "KNIGHT", "ARMSTRONG", "BURNS", "BARKER", "DUNN", "REID" };
        private readonly static string[] mailDomains = new string[127] { "aol.com", "att.net", "comcast.net", "facebook.com", "gmail.com", "gmx.com", "googlemail.com", "google.com", "hotmail.com", "hotmail.co.uk", "mac.com", "me.com", "mail.com", "msn.com", "live.com", "sbcglobal.net", "verizon.net", "yahoo.com", "yahoo.co.uk", "email.com", "games.com", "gmx.net", "hush.com", "hushmail.com", "icloud.com", "inbox.com", "lavabit.com", "love.com", "outlook.com", "pobox.com", "rocketmail.com", "safe-mail.net", "wow.com", "ygm.com", "ymail.com", "zoho.com", "fastmail.fm", "yandex.com", "bellsouth.net", "charter.net", "comcast.net", "cox.net", "earthlink.net", "juno.com", "btinternet.com", "virginmedia.com", "blueyonder.co.uk", "freeserve.co.uk", "live.co.uk", "ntlworld.com", "o2.co.uk", "orange.net", "sky.com", "talktalk.co.uk", "tiscali.co.uk", "virgin.net", "wanadoo.co.uk", "bt.com", "sina.com", "qq.com", "naver.com", "hanmail.net", "daum.net", "nate.com", "yahoo.co.jp", "yahoo.co.kr", "yahoo.co.id", "yahoo.co.in", "yahoo.com.sg", "yahoo.com.ph", "hotmail.fr", "live.fr", "laposte.net", "yahoo.fr", "wanadoo.fr", "orange.fr", "gmx.fr", "sfr.fr", "neuf.fr", "free.fr", "gmx.de", "hotmail.de", "live.de", "online.de", "t-online.de" /* T-Mobile */, "web.de", "yahoo.de", "mail.ru", "rambler.ru", "yandex.ru", "ya.ru", "list.ru", "hotmail.be", "live.be", "skynet.be", "voo.be", "tvcablenet.be", "telenet.be", "hotmail.com.ar", "live.com.ar", "yahoo.com.ar", "fibertel.com.ar", "speedy.com.ar", "arnet.com.ar", "hotmail.com", "gmail.com", "yahoo.com.mx", "live.com.mx", "yahoo.com", "hotmail.es", "live.com", "hotmail.com.mx", "prodigy.net.mx", "msn.com", "yahoo.com.br", "hotmail.com.br", "outlook.com.br", "uol.com.br", "bol.com.br", "terra.com.br", "ig.com.br", "itelefonica.com.br", "r7.com", "zipmail.com.br", "globo.com", "globomail.com", "oi.com.br" };
        private readonly static string[] companyNames = new string[29] { "CHOAM", "Acme Corporation", "Sirius Cybernetics Corporation", "MomCorp", "Rich Industries", "Soylent Corporation", "Very Big Corporation of America", "Frobozz Magic Co.", "Warbucks Industries", "Tyrell Corporation", "Wayne Enterprises", "Virtucon", "Globex Corporation", "Umbrella Corporation", "Wonka Industries", "Stark Industries", "Clampett Oil", "Oceanic Airlines", "Yoyodyne Propulsion Sys.", "Cyberdyne Systems Corporation", "d'Anconia Copper", "Gringotts", "Oscorp", "Nakatomi Trading Corporation", "Spacely", "Initech", "Hooli", "Vehement Capital Partners", "Massive Dynamic" };
        /* sources
         * https://github.com/mailcheck/mailcheck/wiki/List-of-Popular-Domains
         * http://en.geneanet.org/genealogy/1/Surname.php
         * http://names.mongabay.com/male_names_alpha.htm
         * http://www.randomnames.com/all-girls-names.asp
         * */

    }
}
