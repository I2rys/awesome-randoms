        static Dictionary<string, int> proxiez = new Dictionary<string, int>();
        static List<string> verified = new List<string>();
        static string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";
        static Random ran = new Random();

        [STAThread]
        static void Main(string[] args)
        {
            if (!File.Exists("blacklisted.txt"))
            {
                File.WriteAllText("blacklisted.txt", "");
            }
            if (!File.Exists("checked.txt"))
            {
                File.WriteAllText("checked.txt", "");
            }
            if (!File.Exists("proxies.txt"))
            {
                File.WriteAllText("proxies.txt", "");
            }

            string[] accounts = File.ReadAllLines(args[0]);
            Console.Title = "Account Checker by Anon Scripter - [0 / " + accounts.Count() + "] checked...";
            Console.WriteLine("Loaded " + accounts.Count() + " accounts...");
            var proxies = File.ReadAllLines("proxies.txt");

            foreach (var proxy in proxies)
            {
                try
                {
                    proxiez.Add(proxy.Split(':')[0], int.Parse(proxy.Split(':')[1]));
                }
                catch
                {

                }
            }

            Console.WriteLine("Added " + proxiez.Count + " proxies...");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press ENTER whenever your'e ready...");
            Console.ResetColor();
            Console.ReadLine();
            int c = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int i1 = 0; i1 < 20; i1++)
                {
                    Randomize(accounts);
                }
                foreach (string account in accounts)
                {
                    try
                    {
                        var user = account.Split(':')[1];
                        try
                        {
                            KeyValuePair<string, int> proxy = proxiez.ElementAt(ran.Next(0, proxiez.Count - 1));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(string.Format("Checking {0} under {1}", account, proxy.Key + ":" + proxy.Value));
                            Console.ResetColor();

                            if (Check(account, proxy.Key, proxy.Value) && !verified.Contains(account))
                            {
                                verified.Add(account);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(string.Format("New account: {0}", account));
                                Console.ResetColor();
                                if (!File.ReadAllText("checked.txt").Contains(account) && !File.ReadAllText("blacklisted.txt").Contains(account))
                                {
                                    File.AppendAllLines("checked.txt", new string[] { account });
                                }
                            }
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(string.Format("Checking {0} under {1}", account, "no proxy..."));
                            Console.ResetColor();

                            if (Check(account, null, 0) && !verified.Contains(account))
                            {
                                verified.Add(account);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(string.Format("New account: {0}", account));
                                Console.ResetColor();
                                if (!File.ReadAllText("checked.txt").Contains(account) && !File.ReadAllText("blacklisted.txt").Contains(account))
                                {
                                    File.AppendAllLines("checked.txt", new string[] { account });
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                    c++;
                    Console.Title = "Account Checker by Anon Scripter - [" + c + " / " + accounts.Count() + "] checked...";
                }
            }
            while (true)
            {
                Console.ReadLine();
            }
        }

        public static void Randomize<T>(T[] items)
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = ran.Next(i, items.Length);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }
