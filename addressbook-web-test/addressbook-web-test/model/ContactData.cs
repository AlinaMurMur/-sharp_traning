using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using LinqToDB.Mapping;
using WebAddressbookTests;

namespace WebAddessbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allInfo;
        private string name;
        private string phones;
        private string emails;

        public ContactData()
        {
        }
        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return  Lastname == other.Lastname && Firstname == other.Firstname;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Lastname, Firstname);
        }
        public override string ToString()
        {
            return Lastname + " " + Firstname + " ";
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            int result = Lastname.CompareTo(other.Lastname);
            if (result == 0)
            {
                result = Firstname.CompareTo(other.Firstname);
            }
            return result;
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }
        public string HP { 
            get 
            {
                if (HomePhone != "" && HomePhone != null)
                {
                    return "H: " + CleanUpEmpty(HomePhone);
                }
                else
                {
                    return "";
                }
            }
                set
            {
                HomePhone = value;
            }
                }
        public string MP { 
            get
            {
                if (MobilePhone != "" && MobilePhone != null)
                {
                    return "M: " + CleanUpEmpty(MobilePhone);
                }
                else
                {
                    return "";
                }
            }
                set
            {
                MobilePhone = value;
            }
                }
        public string WP {
            get
            {
                if (WorkPhone != "" && WorkPhone != null)
                {
                    return "W: " + CleanUpEmpty(WorkPhone);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                WorkPhone = value;
            }
        }
        public string AllPhones 
        { 
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                } else
                {
                   return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllInfo
        {
            get
            {
                if (allInfo != null)
                {
                    return allInfo;
                }
                return (Name + "\r\n" + CleanUpEmpty(Address) + "\r\n" + CleanUpEmpty(Phones) + CleanUpEmpty(Emails)).Trim();
            }
            set
            {
                allInfo = value;
            }
        }

        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                return (CleanUpEmptyName(Firstname) + " " + CleanUpEmptyName(Lastname)).Trim();
            }
            set
            {
                name = value;
            }
        }
        public string Phones
        {
            get
            {
                if (phones != null)
                {
                    return phones;
                }
                return (HP + MP + WP);
            }
            set
            {
                phones = value;
            }
        }
        public string Emails
        {
            get
            {
                if (emails != null)
                {
                    return emails;
                }
                return (CleanUpEmpty(Email) + CleanUpEmpty(Email2) + CleanUpEmpty(Email3)).Trim();
            }
            set
            {
                emails = value;
            }
        }

        private string CleanUpEmpty(string info)
        {
            {
                if (info == null || info == "")
                {
                    return "";
                }
                return info + "\r\n";
            }
        }

        private string CleanUpEmptyName(string nameInfo)
        {
            {
                {
                    if (nameInfo == null || nameInfo == "")
                    {
                        return "";
                    }
                    return nameInfo;
                }
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "")  + "\r\n";
        }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        //.Where(x => x.Deprecated == "0000-00-00 00:00:00")
                        select c).ToList();
            }
        }

        public static ContactData GetLastContact()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return db.Contacts
                 .OrderByDescending(c => c.Id)
                 .Select(c => new ContactData
                 {
                     Id = c.Id,
                     Firstname = c.Firstname,
                     Lastname = c.Lastname
                 })
                 .First();
            }
        }
    }
}
