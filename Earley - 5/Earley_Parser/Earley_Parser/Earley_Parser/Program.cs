using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Earley_Parser
{
    #region Khoi tao ctdl can thiet
    // ctdl cho du lieu token
    public class db_token :IEquatable<db_token>
    {
        public string name; // gia tri token
        public string type; // loai token

        public db_token(string val, string typ)
        {
            name = val;
            type = typ;
        }

        public bool Equals(db_token item)
        {
            if (name == item.name && type == item.type)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public string Show()
        {
            return type + " -> " + name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    // ctdl cho tap luat sinh
    class rule
    {
        // res --> opt1 opt2
        public string opt1;
        public string opt2;
        public string res;

        public string Show()
        {
            return res + " -> " + opt1 + " " + opt2;
        }
    }

    // ctdl cau
    class sentence
    {
        public List<string> list_string;

        public sentence()
        {
            list_string = new List<string>();
        }

        public sentence(string input)
        {
            list_string = new List<string>();
            string tmp = "";

            for (int i = 0; i < input.Length; i++)
            {                
                if (input[i] != ' ' && input[i] != '$')
                    tmp += input[i].ToString();
                else if (input[i] == ' ' || input[i] == '$')
                {
                    list_string.Add(tmp);
                    tmp = "";
                }
            }
        }
    }

    // ctdl token
    class token
    {
        public string value;        // gia tri goc, phan tach tu cau nhap vao
        public string token_name;   // gia tri token, truy van tu du lieu db_token

        public token()
        {
            value = token_name = "";
        }

        public token(string val, string name)
        {
            value = val;
            token_name = name;
        }

        // so sanh token
        public bool Compare(token t)
        {
            if (value == t.value && token_name == t.token_name)
                return true;
            return false;
        }
    }

    // ctdl bang ketqua
    class node
    {
        public List<token> tokens;

        public node()
        {
            tokens = new List<token>();
        }

        // Kiem tra token co value = S|s co ton tai trong node hay khong
        public bool checkS()
        {
            foreach (token t in tokens)
            {
                if (t.value == "S" || t.value == "s")
                    return true;
            }
            return false;
        }

        // Kiem tra token co value = val co ton tai trong node hay khong
        public bool check(string val)
        {
            foreach (token t in tokens)
            {
                if (t.value == val)
                    return true;
            }
            return false;
        }

        // xuat ra so phan tu hien co trong danh sach token
        public int sopt()
        {
            return tokens.Count;
        }

        // kiem tra token ton tai trong danh sach token, tra ve vi tri cua token
        public int check(token t_check)
        {
            for (int i = 0; i < tokens.Count; i++)
                if (tokens[i].value == t_check.value && tokens[i].token_name == t_check.token_name)
                    return i;
            return -1;
        }

        // xuat ra man hinh toan bo token trong danh sach
        public void Show()
        {
            if (tokens.Count == 0)
                Console.WriteLine("Khong co phan tu!!!");
            else
                foreach (token t in tokens)
                    Console.WriteLine("value: " + t.value + " name: " + t.token_name);
        }

        // xuat ra man hinh toan bo gia tri token trong danh sach
        public void Show2()
        {
            if (tokens.Count == 0)
                Console.WriteLine("Khong co phan tu!!!");
            else
                foreach (token t in tokens)
                    Console.Write(t.value + "\t");
        }

        // them token moi vao danh sach token
        public void Add(token t)
        {
            tokens.Add(t);
        }
                
        // xoa token trong danh sach
        public bool Del(token t)
        {
            int index = check(t);
            if (index == -1)
                return false;
            else
                tokens.RemoveAt(index);
            return true;
        }

        // xoa toan bo token
        public void Del()
        {
            tokens.Clear();
        }

        // so sanh node
        public bool Compare(node n)
        {
            if (tokens.Count != n.tokens.Count)
                return false;
            else
            {
                for (int i=0; i<tokens.Count; i++)
                    if (!tokens[i].Compare(n.tokens[i]))
                        return false;
                return true;
            }
        }
    }

    class tab
    {
        public List<node> data;

        public tab()
        {
            data = new List<node>();
        }       

        // them phan tu vao tab
        public void Add(node n)
        {
            data.Add(n);
        }

        // xoa toan bo phan tu
        public void Del()
        {
            data.Clear();
        }

        // xoa phan tu trong tab theo index
        public bool Del(int index)
        {
            if (index > data.Count)
                return false;
            data.RemoveAt(index);
            return true;
        }

        // xoa phan tu trong tab
        public bool Del(node n)
        {
            int index = check(n);
            if (index == -1)
                return false;
            else
                return Del(index);
        }

        // tim kiem node trong tab tra ve index neu ton tai
        public int check(node n)
        {
            for (int i = 0; i < data.Count; i++)
                if (data[i].Compare(n))
                    return i;
            return -1;
        }

        // xuat bang ket qua
        public void Show()
        {
            foreach (node n in data)
            {
                n.Show2();
                Console.WriteLine();
            }
        }
    }

    #endregion

    class Program
    {
        #region Ham xu ly
        // Doc file luu du lieu phan tach tokens
        static List<db_token> ReadDB(String src, string type)
        {
            string[] lines = File.ReadAllLines(src);
            List<db_token> results = new List<db_token>();
            int i = 0;

            foreach (string line in lines)
            {
                results.Insert(i, new db_token(line, type));
                i++;
            }

            return results;
        }

        // Doc file luu du lieu tap luat sinh
        static List<rule> ReadRules(String src, string type)
        {
            string[] lines = File.ReadAllLines(src);
            List<rule> results = new List<rule>();
            rule tmp2 = new rule();
            int i,j, k;
            i = j = k = 0;
            string tmp = "";

            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    if (c == ' ' || c == '$')
                    {
                        switch (j)
                        {
                            case 0:
                                tmp2.res = tmp;
                                break;
                            case 1:
                                tmp2.opt1 = tmp;
                                break;
                            case 2:
                                tmp2.opt2 = tmp;
                                results.Insert(i, tmp2);
                                break;
                        }
                        j++;
                        tmp = "";
                        k = 0;
                    }
                    else if (c != '$')
                    {
                        tmp = tmp.Insert(k, c.ToString());
                        k++;
                    }
                }
                j = k = 0;
                tmp2 = new rule();
                i++;
            }

            return results;
        }

        static string NhapCau()
        {
            // Nhap cau tu ban phim
            Console.WriteLine("Nhap cau(Ket thuc bang ky tu $ hoac ky tu khoang trang) : ");
            return Console.ReadLine();
        }

        // kiem tra gia tri token co nam trong gia tri res cua tap luat sinh (res -> opt1 opt2)
        static bool checkResRule(string res, List<rule> rules)
        {
            foreach (rule r in rules)
                if (r.res == res)
                    return true;
            return false;
        }

        // kiem tra gia tri token S co nam trong gia tri res cua tap luat sinh (res -> opt1 opt2)
        static bool checkResRule(List<rule> rules)
        {
            foreach (rule r in rules)
                if (r.res == "S")
                    return true;
            return false;
        }

        #endregion

        static void Main()
        {
            #region khai bao, load du lieu ban dau
            // dia chi cac file du lieu token
            string src_noun = @"E:\CaoHoc\Chuong trinh dich\Bai tap\PP Phan tich cu phap Earley\Earley_Parser\nouns.txt";
            string src_verb = @"E:\CaoHoc\Chuong trinh dich\Bai tap\PP Phan tich cu phap Earley\Earley_Parser\verbs.txt";
            string src_adj = @"E:\CaoHoc\Chuong trinh dich\Bai tap\PP Phan tich cu phap Earley\Earley_Parser\adjectives.txt";
            // dia chi cac file du lieu tap luat sinh
            string src_rule = @"E:\CaoHoc\Chuong trinh dich\Bai tap\PP Phan tich cu phap Earley\Earley_Parser\rules.txt";

            // Load tap luat sinh, du lieu token
            // tap danh tu
            List<db_token> nouns = ReadDB(src_noun, "noun");
            // tap dong tu
            List<db_token> verbs = ReadDB(src_verb, "verb");
            // tap trang tu
            List<db_token> adjs = ReadDB(src_adj, "adjective");
            // tap luat sinh
            List<rule> rules = ReadRules(src_rule, "rules");
            #endregion
            
            #region Nhap cau, tien xu ly cau
            // cau nguoi su dung nhap vao
            string input;
            // Cau sau khi da phan tach
            sentence sen;
            // Cau sau khi da nhan dien tokens
            node tokens = new node();

            int flag;
            flag = 0;
            string tmp;
            tmp = "";

            // Nhap cau can xu ly
            input = NhapCau();
            Console.WriteLine("Input: "+ input);

            // Phan tach cau
            sen = new sentence(input);
            // Chuyen doi cau thanh dang token
            #region Chuyen doi cau thanh dang tokens
            foreach (string t in sen.list_string)
            {
                if (flag <= 1)
                {
                    if (nouns.Contains(new db_token(t, "noun")))
                        tokens.Add(new token(t, "N"));
                    else if (verbs.Contains(new db_token(t, "verb")))
                        tokens.Add(new token(t, "V"));
                    else if (adjs.Contains(new db_token(t, "adjective")))
                        tokens.Add(new token(t, "Ad"));
                    else
                    {
                        if (tmp == "")
                            tmp = t;
                        else
                            tmp = tmp + " " + t;
                        flag++;
                    }
                }
                else
                {
                    if (tmp != "")
                    {
                        if (nouns.Contains(new db_token(tmp, "noun")))
                        {
                            tokens.Add(new token(tmp, "N"));
                            flag = 0;
                        }
                        else if (verbs.Contains(new db_token(tmp, "verb")))
                        {
                            tokens.Add(new token(tmp, "V"));
                            flag = 0;
                        }
                        else if (adjs.Contains(new db_token(tmp, "adjective")))
                        {
                            tokens.Add(new token(tmp, "Ad"));
                            flag = 0;
                        }
                        else
                        {
                            flag++;
                            break;
                        }
                        tmp = "";
                    }
                    if (nouns.Contains(new db_token(t, "noun")))
                        tokens.Add(new token(t, "N"));
                    else if (verbs.Contains(new db_token(t, "verb")))
                        tokens.Add(new token(t, "V"));
                    else if (adjs.Contains(new db_token(t, "adjective")))
                        tokens.Add(new token(t, "Ad"));
                    else
                    {
                        if (tmp == "")
                            tmp = t;
                        else
                            tmp = tmp + " " + t;
                    }
                }
            }

            if (tmp != "")
            {
                if (nouns.Contains(new db_token(tmp, "noun")))
                    tokens.Add(new token(tmp, "N"));
                else if (verbs.Contains(new db_token(tmp, "verb")))
                    tokens.Add(new token(tmp, "V"));
                else if (adjs.Contains(new db_token(tmp, "adjective")))
                    tokens.Add(new token(tmp, "Ad"));
                tmp = "";
                flag = 0;
            }
            if (flag == 3)
                Console.WriteLine("Khong the phan tach cau!!!");
            #endregion

            #endregion          

            #region Bang ket qua
            tab result = new tab();
            bool f;
            f = false;  // false : 1; true : 2
            bool f1;    // co danh dau co ket qua hay khong the phan tach cau dua theo bo luat sinh dau vao.
            f1 = false; // mac dinh la khong the phan tach cau
            bool f2;

            for (int i =0; i< tokens.tokens.Count; i++)
            {
                if (f)
                { }
                else
                {
                    if (tokens.tokens[i].value == "S")
                    {
                        // Truong hop hoan thanh banh ket qua.
                        if (tokens.tokens.Count == 1)
                            f1 = true;
                        // Truong hop xet cau phan tich xuat hien S nhung cac phan tu cua cau chua xet het (buoc phan tach hien tai va truoc do phan tach sai)
                        if (i != 1)
                        { }
                        else if (i == 1)
                        // Truong hop cau phan tich xuat hien S nam trong vong 2 buoc
                        break;
                    }
                    else
                    {
                        if (checkResRule(tokens.tokens[i].value, rules))
                        {
                        }
                    }
                }
            }
            #endregion

            #region Tao Bang Earley Parser

            #endregion

            #region Xuat bang Ket qua trich xuat tu Earley Parser
            #endregion

            #region Xuat ket qua
            #endregion

            Console.ReadKey();   // tam dung xem ket qua
        }
    }
}
