using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    int a;
    static Dictionary<int, Item> inventory = new Dictionary<int, Item>();
    static Dictionary<int, Item> shop = new Dictionary<int, Item>();
    private static Character player;   
    public enum ItemType 
    {
        weapon = 1,
        head,
        body 
    }
    public enum ItemPlace
    {
        inventory = 1,
        shop,
    }
    static void Main(string[] args)
    {                                       //공 방 체
        Item item1 = new Item("낡은 검", 2, 0, 0, (int)ItemType.weapon, "Atk +2", "칼이라기 보단 몽둥이에 가까운 검", (int)ItemPlace.inventory,10);
        Item item2 = new Item("무쇠 갑옷", 0, 5, 0, (int)ItemType.body, "Def +5", "무쇠로 만들어져 튼튼한 갑옷", (int)ItemPlace.inventory,10);
        Item item3 = new Item("투구", 0, 0, 0, (int)ItemType.head, "Def +5", "그냥 흔한 투구", (int)ItemPlace.shop,50);
        Item item4 = new Item("버거운 중갑", 0, 30, -30, (int)ItemType.body, "Def +50  Hp-30", "그 갑옷은 갑옷이라기엔 너무 커다랬다", (int)ItemPlace.shop,50);
        Item item5 = new Item("돌(투구)", 0, 20, -20, (int)ItemType.head, "Def +20  Hp-20", "예산을 지키는 탁월한 방법.", (int)ItemPlace.shop,50);
        Item item6 = new Item("가죽 갑옷", 10, 5, 0, (int)ItemType.body, "Atk +10  Def +5", "방어력과 경량화의 균형.", (int)ItemPlace.shop,50);
        Item item7 = new Item("무쇠 갑옷", 0, 5, 0, (int)ItemType.weapon, "Def +5", "칼2", (int)ItemPlace.shop,50);
        Item item8 = new Item("무쇠 갑옷", 0, 5, 0, (int)ItemType.weapon, "Def +5", "칼3", (int)ItemPlace.shop,50);
        Item item9 = new Item("무쇠 갑옷", 0, 5, 0, (int)ItemType.weapon, "Def +5", "칼4", (int)ItemPlace.shop,50);
        //딕셔너리
        inventory.Add(1,item1);
        inventory.Add(2,item2);
        shop.Add(1, item1);
        shop.Add(2, item2);
        shop.Add(3, item3);
        shop.Add(4, item4);
        shop.Add(5, item5);
        shop.Add(6, item6);
        shop.Add(7, item7);
        shop.Add(8, item8);
        shop.Add(9, item9);
  


        Startsetting();
        Village();
    }



    static void Startsetting()
    {
        Console.Clear();
        Console.WriteLine("경비병 : 스파르타 마을에 오신걸 환영합니다.");
        Console.WriteLine("당신의 이름은 무엇인가요?");
        string playerName = Console.ReadLine();
        player= new Character(1, playerName, "전사", 10, 5, 100, 1500);

        Console.WriteLine("1. 입장");
        Console.Write(">>");

        int number = CheckNumber(2);

    }

    static int CheckNumber(int big) 
    {
        string input = Console.ReadLine();
        int number;
        while (!int.TryParse(input, out number))
        {
            Console.WriteLine("숫자를 입력해주세요.");
            input = Console.ReadLine();
        }
        while (number >= big)
        {
            Console.WriteLine("범위 외의 숫자");
            number = CheckNumber(big);
        }

        return number;
    }
    static void Village()
    {
        Console.Clear();
        Console.WriteLine($" {player.Name}님 스파르타 마을에 오신걸 환영합니다.");
        Console.WriteLine("이곳에서 던전에 들어가기 전 활동을 할 수 있습니다");
        Console.WriteLine();
        Console.WriteLine("1. 상태창");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요");
        Console.Write(">>");
        int number = CheckNumber(4);
        switch(number) 
        {
            case 1:
                Status();
                break;

            case 2:
                InventoryShow(inventory);
                break;
            case 3:
                ShopShow(shop);
                break;
        }


    }

    private static void ShopShow(Dictionary<int, Item> shop)
    {
        foreach (KeyValuePair<int, Item> item2 in shop)
        {
            Console.WriteLine("이름{0} 효과 {1} 설명{2}", item2.Value.Name, item2.Value.Effect, item2.Value.Speedweagun);
        }
        //for (int i = 1; i < inventory.Count+1; i++) 
        //{
        //    Console.WriteLine( "이름{0} 효과 {1} 설명{2}", item[i].Name, item[i].Effect , item[i].Speedweagun);
        //}

        Console.Clear();
        Console.WriteLine("아이템목록입니다 ");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");


        int count = shop.Count + 1;

        for (int i = 1; i < shop.Count + 1; i++)
        {
            Console.WriteLine("{0}. {1} 구매 / 판매", i, shop[i].Name);
        }
        int number = CheckNumber(count);


        switch (number)
        {
            case 0:
                Village();
                break;
            case 1:
                shop[number].SellBuy(shop[number], number);
                ShopShow(shop);
                break;
            case 2:
                shop[number].SellBuy(shop[number], number);
                ShopShow(shop);
                break;
            case 3:
                shop[number].SellBuy(shop[number], number);
                ShopShow(shop);
                break;
            case 4:
                shop[number].SellBuy(shop[number], number);
                ShopShow(shop);
                break;
            case 5:
                shop[number].SellBuy(shop[number], number);
                ShopShow(shop);
                break;
            case 6:
                shop[number].SellBuy(shop[number], number);
                ShopShow(shop);
                break;
            case 7:
                shop[number].SellBuy(shop[number], number);
                ShopShow(shop);
                break;
            case 8:
                shop[number].SellBuy(shop[number], number);
                ShopShow(shop);
                break;
            case 9:
                shop[number].SellBuy(shop[number], number);
                ShopShow(shop);
                break;
        }

    }


static void Status()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.Write(">>");
        int number = CheckNumber(1);
        switch (number)
        {
            case 0:
                Village();
                break;
        }
    }
    static void InventoryShow(Dictionary<int, Item> item)
    {
        

        //for (int i = 1; i < inventory.Count+1; i++) 
        //{
        //    Console.WriteLine( "이름{0} 효과 {1} 설명{2}", item[i].Name, item[i].Effect , item[i].Speedweagun);
        //}

        Console.Clear();
        Console.WriteLine("아이템목록입니다 ");
        Console.WriteLine();

        Console.WriteLine("0. 나가기");
        int i = 1;
        foreach (KeyValuePair<int, Item> item2 in item)
        {
            Console.WriteLine("{0}. {1} {2} {3}", i, item2.Value.Name.PadRight(10), item2.Value.Effect.PadRight(20), item2.Value.Speedweagun.PadRight(30));
            ++i;
        }

        Console.Write(">>");

        int count = item.Count+1;
        //for (int i = 1; i < item.Count+1; i++)
        //{
        //   Console.WriteLine("{0}. {1} 장착/해제", i, item[i].Name);
        //}
        int number = CheckNumber(count);


        switch (number)
        {
            case 0:
                Village();
                break;

            case 1:
                inventory[number].EunE(inventory[number], player);
                InventoryShow(item);
                break;
            case 2:
                inventory[number].EunE(inventory[number], player);
                InventoryShow(item);
                break;
            case 3:
                inventory[number].EunE(inventory[number], player);
                InventoryShow(item);
                break;
            case 4:
                inventory[number].EunE(inventory[number], player);
                InventoryShow(item);
                break;
            case 5:
                inventory[number].EunE(inventory[number], player);
                InventoryShow(item);
                break;
            case 6:
                inventory[number].EunE(inventory[number], player);
                InventoryShow(item);
                break;
            case 7:
                inventory[number].EunE(inventory[number], player);
                InventoryShow(item);
                break;
            case 8:
                inventory[number].EunE(inventory[number], player);
                InventoryShow(item);
                break;
            case 9:
                inventory[number].EunE(inventory[number], player);
                InventoryShow(item);
                break;
        }

    }



public class Character  // 레벨 이름  직업 공격력 방어력 체력 골드
{
    public int Level;
    public string Name { get; set; }
    public string Job  { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public int Gold { get; set; }

    public bool typeWeapon=false;
    public bool typehead = false;
    public bool typebody = false;
    public Character(int level, string name, string job, int atk, int def, int hp, int gold)
    {
        Level = level;
        Name = name;
        Job = job;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }
}

    public class Item
    {
        // 이름 공 방 체 타입 구현
        // 타입은 스트링  1 : 무기  2: 갑옷 3:투구
        public bool checkE = false; //트루 장착 펄스 미장착
        public string Name { get; set; }

        public string Effect { get; set; }
        public string Speedweagun { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Type { get; set; }
        public int ItemPlace { get; set; }
        public int Gold { get; set; }
        //public int Code { get; set; }
        public void EunE(Item Item, Character player)
        {
            if (checkE)//장착 중이라면
            {
                if (Item.Type == 1 && player.typeWeapon == true)
                {
                    player.typeWeapon = !player.typeWeapon;
                    player.Atk -= Item.Atk;
                    player.Def -= Item.Def;
                    player.Hp -= Item.Hp;
                    Item.Name = Item.Name.Substring(3, Item.Name.Length - 3);
                    checkE = !checkE;
                    Console.WriteLine("장착 해제");
                    Console.WriteLine("");
                    Console.Write(">>확인");
                    Console.Read();

                }
                else if (Item.Type == 2 && player.typehead == true)
                {
                    player.typehead = !player.typehead;
                    player.Atk -= Item.Atk;
                    player.Def -= Item.Def;
                    player.Hp -= Item.Hp;
                    Item.Name = Item.Name.Substring(3, Item.Name.Length - 3);
                    checkE = !checkE;
                    Console.WriteLine("장착");
                    Console.WriteLine("");
                    Console.Write(">>확인");
                    Console.Read();
                }
                else if (Item.Type == 3 && player.typebody == true)
                {
                    player.typebody = !player.typebody;
                    player.Atk -= Item.Atk;
                    player.Def -= Item.Def;
                    player.Hp -= Item.Hp;
                    Item.Name = Item.Name.Substring(3, Item.Name.Length - 3);
                    checkE = !checkE;
                    Console.WriteLine("장착");
                    Console.WriteLine("");
                    Console.Write(">>확인");
                    Console.Read();
                }
                else
                {
                    Console.WriteLine("뭐임 이거 일어나면 안되는일이 일어나고 있음 장착버그임");
                    Console.WriteLine("");
                    Console.Write(">>확인");
                    Console.Read();
                }

            }
            else //장착중이 아니라면
            {
                if (Item.Type == 1 && player.typeWeapon == false)
                {
                    player.typeWeapon = !player.typeWeapon;
                    player.Atk += Item.Atk;
                    player.Def += Item.Def;
                    player.Hp += Item.Hp;
                    
                    Item.Name = "[E]" + Item.Name;
                    checkE = !checkE;
                }
                else if (Item.Type == 2 && player.typehead == false)
                {
                    player.typehead = !player.typehead;
                    player.Atk += Item.Atk;
                    player.Def += Item.Def;
                    player.Hp += Item.Hp;
                    
                    Item.Name = "[E]" + Item.Name;
                    checkE = !checkE;
                }
                else if (Item.Type == 3 && player.typebody == false)
                {
                    player.typebody = !player.typebody;
                    player.Atk += Item.Atk;
                    player.Def += Item.Def;
                    player.Hp += Item.Hp;
                    
                    Item.Name = "[E]" + Item.Name;
                    checkE = !checkE;
                }
                else
                {
                    Console.WriteLine("이미 무언가를 장착하고 있습니다");
                    Console.WriteLine("");
                    Console.Write(">>확인");
                    Console.Read();
                }

            }
        }
        public Item(string name, int atk, int def, int hp, int type, string effect, string speedweagun, int itemplace, int gold)
        {
            Name = name;
            Atk = atk;
            Def = def;
            Hp = hp;
            Type = type;
            Effect = effect;
            Speedweagun = speedweagun;
            ItemPlace = itemplace;
            Gold = gold;
        }
        public void SellBuy(Item Item, int number)
        {
            if (Item.checkE)
            {
                Console.WriteLine("장착중입니다");
                Console.WriteLine("");
                Console.Write(">>확인");
                Console.Read();
            }
            else
            {
                if (ItemPlace == 1)//소유
                {
                    inventory.Remove(number);
                    player.Gold += Item.Gold / 2;
                    Console.WriteLine($"{Item.Name}을 판매하여 {Item.Gold / 2}원을 획득하였습니다. ");
                    Console.WriteLine($"{player.Gold}을 보유중 입니다.");
                    Console.WriteLine("");
                    Console.Write(">>확인");
                    Console.Read();
                    ItemPlace =2;

                }
                else if (player.Gold >= Item.Gold) { }
                {
                    string test = Item + "{number}";
                    player.Gold -= Item.Gold;
                    inventory.Add(number, Item); //@@@@@@@@@@물어보기

                    Console.WriteLine($"{Item.Name}을 구매하였습니다. ");
                    Console.WriteLine($"{player.Gold}을 보유중 입니다.");
                    Console.WriteLine("");
                    Console.Write(">>확인");
                    Console.Read();
                    ItemPlace = 1;
                }
            }
        }

    }
}