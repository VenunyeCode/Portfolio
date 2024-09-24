using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Constants.Utils;

/// <summary>
/// 
/// </summary>
public static class UtilsConstant
{
    /// <summary>
    /// 
    /// </summary>
    private static readonly Random Random = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <param name="passwordSalt"></param>
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="password"></param>
    /// <param name="storedHash"></param>
    /// <param name="storedSalt"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        if (storedHash.Length != 64) throw new ArgumentException("Longueur non valide du hachage du mot de passe (64 octets attendus).", "storedHash");
        if (storedSalt.Length != 128) throw new ArgumentException("Longueur non valide du sel de mot de passe (128 octets attendus).", "storedSalt");

        using var hmac = new HMACSHA512(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return !computedHash.Where((t, i) => t != storedHash[i]).Any();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string IncrementStringWithNumbers(string str)
    {
        string digits = new(str.Where(char.IsDigit).ToArray());
        string letters = new(str.Where(char.IsLetter).ToArray());

        if (!int.TryParse(digits, out var number)) //int.Parse ferait le travail puisque seuls les chiffres sont sélectionnés
            throw new ArgumentException("Il s'est passé quelque chose d'inattendu");

        return letters + (++number).ToString("D5");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string IncrementNumbers(string str, string size)
    {
        string digits = new(str.Where(char.IsDigit).ToArray());

        if (!int.TryParse(digits, out var number)) //int.Parse ferait le travail puisque seuls les chiffres sont sélectionnés
            throw new ArgumentException("Il s'est passé quelque chose d'inattendu");

        return (++number).ToString("D" + size);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modele"></param>
    /// <param name="agents"></param>
    /// <param name="lastMatricule"></param>
    /// <returns></returns>
    public static string ComposeMatricule(string modele, List<string> agents, string lastMatricule = null)
    {
        if (string.IsNullOrEmpty(modele))
            return null;

        var formats = modele.Split("-");
        var nom = formats[0].Trim();
        var prenom = formats[1].Trim();
        var sequence = formats[2].Trim();

        var number = int.Parse(new string(nom.Where(char.IsDigit).ToArray()));
        nom = agents.ElementAt(0)[..number];
        number = int.Parse(new string(prenom.Where(char.IsDigit).ToArray()));
        prenom = agents.ElementAt(1)[..number];
        if (string.IsNullOrEmpty(lastMatricule))
            return nom.ToUpper() + prenom.ToUpper();

        sequence = IncrementNumbers(lastMatricule, new string(sequence.Where(char.IsDigit).ToArray()));

        return nom.ToUpper() + prenom.ToUpper() + '-' + sequence;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string UppercaseWords(string value) => string.Join(" ", value.Split(' ').ToList().ConvertAll(word => word[..1].ToUpper() + word[1..].ToLower()));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="length"></param>
    /// <param name="nonAlphaNumericChars"></param>
    /// <returns></returns>
    public static string GeneratePassword(int length, int nonAlphaNumericChars)
    {
        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        const string allowedNonAlphaNum = "!@#$%^&*()_-+=[{]};:<>|./?";
        var pass = string.Empty;

        Random rd = new(DateTime.Now.Millisecond);
        for (var i = 0; i < length; i++)
        {
            if (nonAlphaNumericChars > 0)
            {
                pass += allowedNonAlphaNum[rd.Next(allowedNonAlphaNum.Length)];
                nonAlphaNumericChars--;
            }
            else
                pass += allowedChars[rd.Next(allowedChars.Length)];
        }

        return pass;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="photoUrl"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static async Task<string> GetImageFromUrl(string photoUrl, int width = 50, int height = 50)
    {
        string photo;
        if (string.IsNullOrEmpty(photoUrl))
            photo = "<img src=\"../../../app-assets/images/avatars/avatar.png\" " +
            "class=\"uploadedAvatar rounded me-50\" alt=\"profile image\" height=\"" + height + "\" width=\"" + width + "\" />";
        else
        {
            var base64 = Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync(photoUrl));
            var image = $"data:image/gif;base64,{base64}";
            photo = "<div class=\"text-center\"><div class=\"avatar avatar-lg\"><img src=\"" + image + "\" alt=\"avatar\" /></div></div>";
        }

        return photo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static decimal FromPercentageString(this string value) => decimal.Parse(value.Replace("%", "").Trim()) / 100;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ba0"></param>
    /// <param name="ba1"></param>
    /// <returns></returns>
    public static bool CompareByteArrays(byte[] ba0, byte[] ba1) => !(ba0.Length != ba1.Length || Enumerable.Range(1, ba0.Length).FirstOrDefault(n => ba0[n] != ba1[n]) > 0);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static int GetOnlyNumbers(string input)
    {
        var stringBuilder = new StringBuilder(input.Length);
        foreach (var t in input.Where(t => t is >= '0' and <= '9'))
            stringBuilder.Append(t);

        return int.Parse(stringBuilder.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => self.Select((item, index) => (item, index));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static decimal? IsDecimalNull(decimal? expression, decimal? value) => expression ?? value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static double? IsDoubleNull(double? expression, double? value) => expression ?? value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int? IsIntNull(int? expression, int? value) => expression ?? value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static bool? IsBoolNull(bool? expression) => expression ?? false;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool In<T>(this T source, params T[] list)
    {
        if (null == source) throw new ArgumentNullException(nameof(source));

        return list.Contains(source);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable == null || !enumerable.Any();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNull(this object obj) => obj == null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNullOrZero(this object obj)
    {
        var parsed = int.TryParse(obj.ToString(), out int parseInt);

        return IsNull(obj) || (parsed && parseInt == 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNum(this object obj) => int.TryParse(obj.ToString(), out var num);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsDecimal(this object obj) => decimal.TryParse(obj.ToString(), out var num);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || !list.Any();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this List<Dictionary<string, string>> dic) => dic == null || !dic.Any();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public static long GetColumnNumber(string columName)
    {
        var chars = columName.ToUpper().ToCharArray();
        var number = (long)(Math.Pow(26, chars.Length - 1))
                     * Convert.ToInt32(chars[0] - 64)
                     + (chars.Length > 2 ? GetColumnNumber(columName.Substring(1, columName.Length - 1))
                         : chars.Length == 2 ? Convert.ToInt32(chars[^1] - 64)
                         : 0);

        return number;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnNumber"></param>
    /// <returns></returns>
    public static string GetColumnName(long columnNumber)
    {
        var val = new StringBuilder();

        for (var n = (int)(Math.Log(25 * (columnNumber + 1)) / Math.Log(26)) - 1; n >= 0; n--)
        {
            var x = (int)((Math.Pow(26, n + 1) - 1) / 25 - 1);
            if (columnNumber > x)
                val.Append(Convert.ToChar((int)(((columnNumber - x - 1) / Math.Pow(26, n)) % 26 + 65)));
        }

        return val.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static (DateTime minDate, DateTime maxDate) GetSemestrialsDate(DateTime date) => (new DateTime(date.Year, 1 + 6 * (date.Month / 7), 1),
        new DateTime(date.Year, 7 - 6 * (date.Month / 7), 1).AddDays(-1.0));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="originalDate"></param>
    /// <param name="quarters"></param>
    /// <returns></returns>
    public static DateTime AddQuarters(DateTime originalDate, int quarters) => originalDate.AddMonths(quarters * 3);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static int GetQuarter(DateTime date)
    {
        var month = date.Month - 1;
        var month2 = Math.Abs(month / 3) + 1;

        return month2;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="originalDate"></param>
    /// <returns></returns>
    public static DateTime GetFirstDayOfQuarter(DateTime originalDate) => AddQuarters(new DateTime(originalDate.Year, 1, 1), GetQuarter(originalDate) - 1);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="originalDate"></param>
    /// <returns></returns>
    public static DateTime GetLastDayOfQuarter(DateTime originalDate) => AddQuarters(new DateTime(originalDate.Year, 1, 1), GetQuarter(originalDate)).AddDays(-1);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="e1"></param>
    /// <param name="e2"></param>
    /// <returns></returns>
    public static bool Compare<T>(T e1, T e2)
    {
        var flag = true;
        int countFirst, countSecond;
        foreach (PropertyInfo propObj1 in e1.GetType().GetProperties())
        {
            var propObj2 = e2.GetType().GetProperty(propObj1.Name);
            if (propObj1.PropertyType.Name.Equals("List`1"))
            {
                dynamic objList1 = propObj1.GetValue(e1, null);
                dynamic objList2 = propObj2.GetValue(e2, null);
                countFirst = objList1.Count;
                countSecond = objList2.Count;
                if (countFirst == countSecond)
                {
                    countFirst = objList1.Count - 1;
                    while (countFirst > -1)
                    {
                        bool match = false;
                        countSecond = objList2.Count - 1;
                        while (countSecond > -1)
                        {
                            match = Compare(objList1[countFirst], objList2[countSecond]);
                            if (match)
                            {
                                objList2.Remove(objList2[countSecond]);
                                countSecond = -1;
                                match = true;
                            }
                            if (match == false && countSecond == 0)
                            {
                                return false;
                            }
                            countSecond--;
                        }
                        countFirst--;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (!(propObj1.GetValue(e1, null).Equals(propObj2.GetValue(e2, null))))
            {
                flag = false;

                return flag;
            }
        }

        return flag;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vals"></param>
    /// <param name="preds"></param>
    /// <returns></returns>
    public static List<T> FindAll<T>(this IQueryable<T> vals, List<Predicate<T>> preds)
    {
        List<T> data = new();
        foreach (T e in vals)
        {
            bool pass = true;
            foreach (Predicate<T> p in preds)
            {
                if (!(p(e)))
                {
                    pass = false;
                    break;
                }
            }

            if (pass) data.Add(e);
        }

        return data;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static string GetFullMessage(this Exception ex) => "(" + ex.GetType().Name.ToString() + ") --> " + (ex.InnerException == null ? ex.Message : ex.InnerException.GetFullMessage());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string RemoveAccentuation(string text) => System.Web.HttpUtility.UrlDecode(System.Web.HttpUtility.UrlEncode(text, Encoding.GetEncoding("iso-8859-7")));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="destinationDir"></param>
    public static void CopyFilesAndDirectories(this DirectoryInfo directory, string destinationDir)
    {
        var directories = Directory.GetDirectories(directory.FullName, "*", SearchOption.AllDirectories);
        foreach (string dir in directories)
        {
            string dirToCreate = dir.Replace(directory.FullName, destinationDir);
            Directory.CreateDirectory(dirToCreate);
        }

        var files = Directory.GetFiles(directory.FullName, "*.*", SearchOption.AllDirectories);
        foreach (string newPath in files)
        {
            File.Copy(newPath, newPath.Replace(directory.FullName, destinationDir), true);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="zipName"></param>
    /// <param name="filePaths"></param>
    /// <returns></returns>
    public static (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string zipName, List<string> filePaths)
    {
        using var memoryStream = new MemoryStream();
        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            filePaths.ForEach(filePath =>
            {
                var theFile = archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath), CompressionLevel.Optimal);
            });
        }

        return ("application/zip", memoryStream.ToArray(), zipName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string EncodeNonAsciiCharacters(string value)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in value)
        {
            if (c > 127)
            {
                string encodedValue = "\\u" + ((int)c).ToString("x4");
                sb.Append(encodedValue);
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string DecodeEncodedNonAsciiCharacters(string value)
    {
        return Regex.Replace(
            value,
            @"\\u(?<Value>[a-zA-Z0-9]{4})",
            m =>
            {
                return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
            });
    }

    /// <summary>
    /// Arrondissement d'une valeur à l'intervalle de 10 le plus proche.
    /// le paramètre roundedDown est à false par défaut.
    /// </summary>
    /// <param name="number"></param>
    /// <param name="interval"></param>
    /// <param name="roundedDown"></param>
    /// <returns></returns>
    public static int RoundOff(int number, int interval, bool roundedDown = false)
    {
        int remainder = number % interval;
        if(!roundedDown)
            number += (remainder < 10 / 2) ? -remainder : (10 - remainder);
        else
            number += -remainder;

        return number;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="datetime"></param>
    /// <returns></returns>
    public static bool IsValidDateTime(this string datetime)
    {
        string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "d/M/yy", "dd/MM/yy", "mm/dd/yyyy", "dd.mm.yyyy", 
            "dd/MM/yyyy h:mm:ss tt", "d/M/yyyy h:mm:ss tt", "d/M/yy h:mm:ss tt", "dd/MM/yy h:mm:ss tt",
            "dd/MM/yyyy h:mm tt", "d/M/yyyy h:mm tt", "d/M/yy h:mm tt", "dd/MM/yy h:mm tt",
            "dd/MM/yyyy hh:mm:ss", "d/M/yyyy hh:mm:ss", "d/M/yy hh:mm:ss", "dd/MM/yy hh:mm:ss",
            "dd/MM/yyyy h:mm:ss", "d/M/yyyy h:mm:ss", "d/M/yy h:mm:ss", "dd/MM/yy h:mm:ss", 
            "dd/MM/yyyy h:mm:ss", "d/M/yyyy h:mm:ss", "d/M/yy h:mm:ss", "dd/MM/yy h:mm:ss", 
            "dd/MM/yyyy hh:mm tt", "d/M/yyyy hh:mm tt", "d/M/yy hh:mm tt", "dd/MM/yy hh:mm tt", 
            "dd/MM/yyyy hh tt", "d/M/yyyy hh tt", "d/M/yy hh tt", "dd/MM/yy hh tt", 
            "dd/MM/yyyy h:mm", "d/M/yyyy h:mm", "d/M/yy h:mm", "dd/MM/yy h:mm",
            "dd/MM/yyyy hh:mm", "d/M/yyyy hh:mm", "d/M/yy hh:mm", "dd/MM/yy hh:mm"
        };

        if (DateTime.TryParseExact(datetime, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            return true;

        return false;
    }
}
