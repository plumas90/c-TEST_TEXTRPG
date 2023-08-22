using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    //태초에 딕셔너리 인벤토리와 샵을 만들어줌
    static Dictionary<int, Item> inventory = new Dictionary<int, Item>();
    static Dictionary<int, Item> shop = new Dictionary<int, Item>();

    static List<Item> inventorylist = new List<Item>(); //리스트의 흔적 난 딕셔너리를 고르고 후회했으며 리스트로 바꿔보려는 시도를 했으나 방대한 양에 작업도중 포기를 선언한다
    static List<Item> ShopList = new List<Item>();     // 앞으로 나올 리스트는 모두 실패의 흔적이다
    private static Character player;
    public enum ItemType // 아이템 생성시 1 2 3 은 불편하여 선언해줌 그러나 놀랍게도 많이 사용한일은 없었다
    {
        weapon = 1,
        head,
        body
    }
    public enum ItemPlace// 소유를구별하기 위해 아이템위치를 만들어줌 그러나 enum선언위치를 프로그램 네에 하여 메인 제외에선 1 2 로 사용되었다
    {
        inventory = 1,
        shop,
    }
    static void Main(string[] args)
    {    
        //아이템 배열 관리하기
        //만들기
        Item item1 = new Item("낡은 검 ", 2, 0, 0, (int)ItemType.weapon, "Atk +2", "칼이라기 보단 몽둥이에 가까운 검", (int)ItemPlace.inventory, 10,1);
        Item item2 = new Item("무쇠 갑옷 ", 0, 5, 0, (int)ItemType.body, "Def +5", "무쇠로 만들어져 튼튼한 갑옷", (int)ItemPlace.inventory, 10,2);
        Item item3 = new Item("투구", 0, 0, 0, (int)ItemType.head, "Def +5", "그냥 흔한 투구", (int)ItemPlace.shop, 50,3);
        Item item4 = new Item("버거운 중갑 ", 0, 30, -30, (int)ItemType.body, "Def +50  Hp-30", "그 갑옷은 갑옷이라기엔 너무 커다랬다", (int)ItemPlace.shop, 50,4);
        Item item5 = new Item("돌(투구)  ", 0, 20, -20, (int)ItemType.head, "Def +20  Hp-20", "예산을 지키는 탁월한 방법.", (int)ItemPlace.shop, 50,5);
        Item item6 = new Item("가죽 갑옷 ", 10, 5, 0, (int)ItemType.body, "Atk +10  Def +5", "방어력과 경량화의 균형.", (int)ItemPlace.shop, 50,6);
        Item item7 = new Item("면도날", 5, 0, 0, (int)ItemType.weapon, "Atk +5", "엣날엔 면도기라고 하면 한날이 기본이었다", (int)ItemPlace.shop, 50,7);
        Item item8 = new Item("이중 면도날 ", 10, 0, 0, (int)ItemType.weapon, "Atk +10", "누군가가 날 두개를 겹치면 날이 하나일때 보다 더 잘된다는것을 발견했다", (int)ItemPlace.shop, 100,8);
        Item item9 = new Item("삼중 면도날 ", 20, 0, 0, (int)ItemType.weapon, "Atk +20", "면도날에 있어 더 이상 개선의 여지는 없는것 처럼 보였다", (int)ItemPlace.shop, 200,9);
        Item item10 = new Item("오중 면도날 ", 30, 0, 0, (int)ItemType.weapon, "Atk +30", "누가 면도날의 갯수가 4개에서 5개로 늘어날 수 있다고 상상이나 했을까?", (int)ItemPlace.shop, 300,10);
        //딕셔너리
        
        inventorylist.Add(item1);
        inventorylist.Add(item2);
        //ShopList.Add(item1);
        //ShopList.Add(item2);
        //ShopList.Add(item3);
        //ShopList.Add(item4);
        //ShopList.Add(item5);
        //ShopList.Add(item6);
        //ShopList.Add(item7);
        //ShopList.Add(item8);
        //ShopList.Add(item9);
        //ShopList.Add(item10);
        //.FindIndex(a => a.Code("1")
        //기본 아이템은 인벤토리에만 넣어주고 상점에선팔기를 위해 소유중인 아이템도 넣어준다
        inventory.Add(1, item1);
        inventory.Add(2, item2);
        shop.Add(1, item1);
        shop.Add(2, item2);
        shop.Add(3, item3);
        shop.Add(4, item4);
        shop.Add(5, item5);
        shop.Add(6, item6);
        shop.Add(7, item7);
        shop.Add(8, item8);
        shop.Add(9, item9);
        shop.Add(10, item10);

        Startsetting();
        Village();
    }



    static void Startsetting() // 게임 실행시 처음 실행되며 처음 캐릭터 값을 입력해준다
    {

        Console.Clear();
        Console.WriteLine("경비병 : 스파르타 마을에 오신걸 환영합니다.");
        Console.WriteLine("당신의 이름은 무엇인가요?");
        string playerName = Console.ReadLine();
        player = new Character(1, playerName, "전사", 10, 5, 100, 1500);
        Console.WriteLine("1. 입장");
        Console.Write(">>");

        int number = CheckNumber(2);

    }

    static int CheckNumber(int big)     // 입력값 정상 비정상 판별
                                        // 선택지가 2까지라면 그보다 1큰 3을 넣어준다 허나 버그 수정으로 의미없는 숫자가 되버림
    {
        string input = Console.ReadLine();
        int number;
        while (!int.TryParse(input, out number))
        {
            Console.WriteLine("범위에 맞는 '숫자'를 입력해주세요.");
            input = Console.ReadLine();
        }
        // 위에 까지는 흔히 문자 제외 받는 구성 이였으나 나는 그렇다면 1까지 선택지가 있으면 어떻게 하지? 란생각에 아래가 추가됨

        //while (number >= big)
        //{
        //    Console.WriteLine("범위 외의 숫자");
        //    number = CheckNumber(big); // 범위 외에 숫자일시 재귀를 통해 다시한번 입력 받을수 있음
        //}

        return number;
    }
    static void Village()   //메인 마을씬 젤 처음 만들어진 함수로 다른 씬 함수들이 보여주는 show가 붙은걸 생각하면 이친구도 붙여줘야 하나 너무 먼곳으로 가버림
    {
        player.Hpcheck();
        Console.Clear();
        Console.WriteLine($" {player.Name}님 스파르타 마을에 오신걸 환영합니다.");
        Console.WriteLine("이곳에서 던전에 들어가기 전 활동을 할 수 있습니다");
        Console.WriteLine();
        Console.WriteLine("1. 상태창");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 던전");
        Console.WriteLine("5. 여관");

        Console.WriteLine("원하시는 행동을 입력해주세요");
        Console.Write(">>");
        int number = CheckNumber(6);
        switch (number)
        {
            case 1:
                Status();
                break;

            case 2:
                InventoryShow(inventory);
                break;
            case 3:
                ShopShow(shop,inventorylist);
                break;
            case 4:
                dungeonShow();
                break;
            case 5:
                restShow();
                break;
            default: //상점 구현 전까진 체크넘버가 범위외 숫자를 처리해주었으나 버그로 인해 예외 처리를 해주었다 
                try
                {
                    Console.WriteLine("입력값이 다릅니다");
                    Console.ReadLine();
                    Village();
                    break;
                }
                catch (Exception)
                {

                    throw;
                    break;
                }
        }


    }
    public static void restShow() // 여관 씬으로 이동 처음 생성시 휴식만 생각하고 만들고 휴식장소를 생각안해서 rest가 된 케이스
    {
        Console.Clear();
        Console.WriteLine("[여관]");
        Console.WriteLine("");
        Console.WriteLine("휴식 : 체력 회복 ( 500 G)");
        Console.WriteLine("");
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 휴식");



        int number = CheckNumber(2);
        switch (number)
        {
            case 0:
                Village();
                break;
            case 1:
                rest();
                break;
            default:
                try
                {
                    Console.WriteLine("입력값이 다릅니다");
                    Console.ReadLine();
                    restShow();
                    break;
                }
                catch (Exception)
                {

                    throw;
                    break;
                }
        }
    }
    public static void dungeonShow()  // 던전입장 
    {
        Console.Clear();
        Console.WriteLine("던전 입장");
        Console.WriteLine("");
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 쉬운 던전     ★  | 방어력 5 이상 권장");
        Console.WriteLine("2. 일반 던전    ★★ | 방어력 11 이상 권장");
        Console.WriteLine("3. 어려운 던전 ★★★| 방어력 17 이상 권장");
        Console.WriteLine($"현재 방어력 : {player.Def}");
        Console.WriteLine("");
        Console.WriteLine(">>");
        int number = CheckNumber(4);
        switch (number)
        {
            case 0:
                Village();
                break;
            case 1:
            case 2:
            case 3:
                dungeonclear(number);
                break;
            default:
                try
                {
                    Console.WriteLine("입력값이 다릅니다");
                    Console.ReadLine();
                    dungeonShow();
                    break;
                }
                catch (Exception)
                {

                    throw;
                    break;
                }
        }



    }
    public static void dungeonclear(int stage) //던전 성공 여부를 난이도에 따라 다르게 처리해주는 함수 던전쇼에서 난이도에 따라 1 2 3을 스테이지로 받아오기에 안심
    {
        Console.Clear();
        bool clear1 = false;
        Random damege = new Random(); //원하는 랜덤을 위해 랜덤을 여러개 만들었는데 생각해보니 1개만 있어도 됨
        Random Rclear = new Random();
        Random pumpkin = new Random();
        pumpkin.Next(1, 2);
        int pumpkin1 = pumpkin.Next(1, 2);
        int damege1 = damege.Next(20, 35);
        string star = "";
        int giveMoney = 0;
        int realdamege = 0 ;
        int Rclear1 = Rclear.Next(0, 100);
        Console.WriteLine(Rclear);
        switch (stage)// 난이도별 변수 설정 쉬움 노말 어려움
        {
            case 1:
                Console.WriteLine("들어옴체크1");
                giveMoney = 1000;
                giveMoney *= pumpkin1;
                star = "쉬운";
                if (player.Def >= 5) //권장방어력보다높다면
                {
                    clear1 = true;
                    realdamege = damege1 - (player.Def - 5);
                    if (realdamege < 0)
                    {
                        realdamege = 0;
                    }
                    //Console.WriteLine("들어옴체크1");

                }
                else if (Rclear1 >= 41)
                {
                    clear1 = true;
                    realdamege = damege1 - (player.Def - 5);
                    if (realdamege < 0)
                    {
                        realdamege = 0;
                    }
                    //Console.WriteLine("들어옴체크1");
                }
                else
                {
                    realdamege = player.Hp / 2;
                }

                break;

                  case 2:
                 giveMoney = 1500;
                 giveMoney *= pumpkin1;
                   star = "일반";
                 if (player.Def >= 11) //권장방어력보다높다면
                 {
                    clear1 = true;
                    realdamege = damege1 - (player.Def - 5);
                    if (realdamege < 0)
                    {
                        realdamege = 0;
                    }

                 }
                 else if (Rclear1 >= 41)
                 {
                    clear1 = true;
                    realdamege = damege1 - (player.Def - 11);
                    if (realdamege < 0)
                    {
                        realdamege = 0;
                    }
                  }
                else
                {
                    realdamege = player.Hp / 2;
                }

                break;

                 case 3:
                 giveMoney = 2500;
                 giveMoney *= pumpkin1;
                 star = "어려운";
                 if (player.Def >= 17) //권장방어력보다높다면
                  {
                  clear1 = true;
                 realdamege = damege1 - (player.Def - 17);
                 if (realdamege < 0)
                 { 
                    realdamege = 0;
                 }

                 }
                 else if (Rclear1 >= 41)
                 {
                  clear1 = true;
                  realdamege = damege1 - (player.Def - 5);
                  if (realdamege < 0)
                    {
                    realdamege = 0;
                    }
                  }
                  else
                 {
                  realdamege = player.Hp / 2;
                 }
                 break;
        }
        // 공격력 비례 골드 배율
        if (player.Atk >= 30)
        {
            giveMoney = (int)1.5 * giveMoney;
        }
        else if (player.Atk >= 20)
        {
            giveMoney = (int)1.3*giveMoney;
        }
        else if (player.Atk >= 20)
        {
            giveMoney = (int)1.1 * giveMoney;
        }
        bool levelFlag = false;//레벨업 체크 
        if(player.Level<=4)
        {
            levelFlag = true;
        }



        player.NowHp -= realdamege; // 데미지값이 경우에 따라 다르기에 이친구는 스위치문 후 처리해줌 
            Console.Clear();
            if (player.NowHp <= 0) //죽었을때 체크 
            { 
                Console.WriteLine($"{realdamege}만큼 피해를 입어 체력이 {player.NowHp}이 되어 정신을 잃었습니다");
            player.NowHp = player.Hp;
            levelFlag = false;//죽었다면 레벨플래그 펄스
                Console.WriteLine($"골드를 {player.Gold/2}를 잃었다.");
                player.Gold /= 2;
                    if (clear1 == true)
                   {
                    clear1 = !clear1;
                   }
            }

        if (clear1 == true)
         {
         Console.Clear();
         Console.WriteLine("던전 클리어");
         Console.WriteLine("축하합니다!!!");
         Console.WriteLine("{0} 던전을 클리어 하였습니다.",star);
         Console.WriteLine("");
         Console.WriteLine("[탐험 결과]");
            if (levelFlag) 
            {
                player.levelUp();
                Console.WriteLine("야호! 레벨이 올랐다!");
                Console.WriteLine($"레벨 : {player.Level - 1} -> {player.Level}");
                Console.WriteLine($"Atk : {player.Atk - 2} -> {player.Atk}");
                Console.WriteLine($"Atk : {player.Def - 2} -> {player.Def}");
            }
        Console.WriteLine($"체력 {player.NowHp + realdamege} -> {player.NowHp}");
        Console.WriteLine("Gold {0} -> {1}", player.Gold - giveMoney, player.Gold);
         player.Gold += giveMoney;
         }
         else 
          {
             Console.WriteLine("실패!");
          }

        
        Console.WriteLine("");
        Console.WriteLine("0.나가기");
        int number = CheckNumber(2);
        switch (number)
        {
            case 0:
                dungeonShow();
                break;
            default:
                try
                {
                    Console.WriteLine("입력값이 다릅니다");
                    Console.ReadLine();
                    dungeonclear(stage);
                    break;
                }
                catch (Exception)
                {

                    throw;
                    break;
                }
        }
    }
    public static void rest()
    {

        if (player.Gold >= 500)
        {
            Console.WriteLine("휴식을 완료했습니다.");
            player.Gold -= 500;
            player.NowHp = player.Hp;
        }
        else
        {
            Console.WriteLine("Gold가 부족합니다.");
        }
        Console.WriteLine($"현재 {player.Gold}G 소지중");
        Console.WriteLine("");
        Console.WriteLine(">>확인");
        Console.ReadLine();
        restShow();


    }
    private static void ShopShow(Dictionary<int, Item> shop, List<Item> inventorylist)
    {
        //for (int i = 1; i < inventory.Count+1; i++) 
        //{
        //    Console.WriteLine( "이름{0} 효과 {1} 설명{2}", item[i].Name, item[i].Effect , item[i].Speedweagun);
        //}
        Console.Clear();
        Console.WriteLine("아이템목록입니다. ");
        Console.WriteLine("판매시 구매 가격의 50%로 구매해드립니다. ");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        foreach (KeyValuePair<int, Item> item2 in shop)
        {
            if (item2.Value.ItemPlace == 1)//보유체크
            {
                Console.Write("[보유]");
            }
            else 
            {
                Console.Write("[미보유]");
            }
            Console.WriteLine("{0}. {1} {2} {3} {4}G 구매 / 판매",item2.Value.Code, item2.Value.Name.PadRight(20 - item2.Value.Name.Length), item2.Value.Effect.PadRight(30 - item2.Value.Effect.Length), item2.Value.Speedweagun.PadRight(60 - item2.Value.Speedweagun.Length),item2.Value.Gold);
        }
        Console.WriteLine();
        Console.Write(">>");

        int count = shop.Count + 1;

        //for (int i = 1; i < shop.Count + 1; i++)
        //{
        //    Console.WriteLine("{0}. {1} 구매 / 판매", i, shop[i].Name);
        // }
        int number = CheckNumber(count);


        switch (number)
        {
            case 0:
                Village();
                break;
            case 1:
                shop[number].SellBuy(shop[number], number,inventorylist);
                ShopShow(shop,inventorylist);
                break;
            default:
                try
                {
                    shop[number].SellBuy(shop[number], number, inventorylist);
                    ShopShow(shop, inventorylist);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("입력 값이 다릅니다");
                    Console.ReadLine();
                    ShopShow(shop, inventorylist);
                    break;
                    throw;
                }


           
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
        Console.WriteLine($"공격력 :{player.Atk} (+{player.Atk-10-player.Level*2+2})");
        Console.WriteLine($"방어력 : {player.Def} (+{player.Def-5-player.Level*2+2})");
        Console.WriteLine($"체력 : {player.NowHp}/{player.Hp}");
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
        foreach (KeyValuePair<int, Item> item2 in item)
        {
            Console.WriteLine("{0}. {1}  {2}  {3}  장착/해제", item2.Value.Code, item2.Value.Name.PadRight(20- item2.Value.Name.Length), item2.Value.Effect.PadRight(30- item2.Value.Effect.Length), item2.Value.Speedweagun.PadRight(40-item2.Value.Speedweagun.Length));
            
        }

        Console.Write(">>");

        int count = item.Count + 1;
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
            default :
                try
                {
                    inventory[number].EunE(inventory[number], player);
                    InventoryShow(item);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("입력 값이 다릅니다");
                    Console.ReadLine();
                    InventoryShow(item);
                    break;
                    throw;
                }



        }
        

    }



    public class Character  // 레벨 이름  직업 공격력 방어력 체력 골드
    {
        public int Level;
        public string Name { get; set; }
        public string Job { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int NowHp { get; set; }
        public int Gold { get; set; }

        public bool typeWeapon = false;
        public bool typehead = false;
        public bool typebody = false;

        public void Hpcheck()
        {
            if (player.Hp < player.NowHp)
            {
                player.NowHp = player.Hp;
            }
        }
        
        public Character(int level, string name, string job, int atk, int def, int hp, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            NowHp = hp;
        }
        

        public void levelUp() 
        {
            if (player.Level <= 4) 
            {
                player.Level++;
                player.Atk += 2;
                player.Def += 2;
            }
        }
    }

    public class Item
        //처음엔 인터페이스를 받아서 사용했으나 아이템 종류가 장비템 뿐이라 코드낭비로 삭제되었다
    {
        //태초엔 아이템에 이름 공 방 체 아이템코드 만구현 됬다 허나 코드는 사용되지 않았었음 아래는 점차 필요하에 늘어났다
        public string Name { get; set; } 
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        //public int Code { get; set; } // 게임내에 흔히 아이템 코드 (ex 게임내 치트로 아이템 불러올시)가 있어서 넣어볼려고 했었음


        public bool checkE = false; //트루 장착 펄스 미장착
        public string Effect { get; set; }//효과 (공 +5)
        public string Speedweagun { get; set; }// 설명

        // 이름 공 방 체 타입 구현
        // 타입은 스트링  1 : 무기  2: 갑옷 3:투구
        public int Type { get; set; }
        public int ItemPlace { get; set; } // 장소 난 이것으로 인벤토리 배열 에 넣을것인지 상점배열에 넣을것인지를 구별함 매우 애용한 기능 
        public int Gold { get; set; } // 가격
        public int Code { get; set; } // 아이템 코드 입력시 문자열이 딕셔너리 선택으로 인한 버그가 발생하여 대용하기위한 마지막에 추가된 속성


        public void EunE(Item Item, Character player)
        {
            if (checkE)//장착 중이라면 
            {
                if (Item.Type == 1 && player.typeWeapon == true)//아이템 타입을 1무기 2헬멧 3 방어구 로 구현
                {
                    player.typeWeapon = !player.typeWeapon;  //그 부분에 장착
                    player.Atk -= Item.Atk;  //나는 아이템 장착시 플레이어 공격력에 직접 올려주거나 빼주거나 하는 식으로 구현 다른 케이스가 있다면 궁금함
                    player.Def -= Item.Def;
                    player.Hp -= Item.Hp;
                    Item.Name = Item.Name.Substring(3, Item.Name.Length - 3);  // 장착시 앞에 [E] 를 붙여줌 즉 장착하면 아이템의 이름이 바뀌는 구조 가 -> [E]가 -> 가
                                                                               // 다른분들의 구현이 궁금한 부분
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
                if (Item.Type == 1 && player.typeWeapon == false)// 장착의 역순 더한걸빼주고 펄스를 트루로 트루를 펄스로 
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
        public Item(string name, int atk, int def, int hp, int type, string effect, string speedweagun, int itemplace, int gold , int code)
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
            Code = code;
        }
        public void SellBuy(Item Item, int number, List<Item> inventorylist) //판매/구매
        {
            if (Item.checkE) //아이템의장착 여부 확인 
            {
                Console.WriteLine("장착중입니다");
                Console.WriteLine("");
                Console.Write(">>확인");
                Console.Read();
            }
            else//보유시 판매를 미보유시 구매를 
            {
                if (ItemPlace == 1)//보유
                {   //무한으로 판매되는 버그가 있었으나 고쳐짐 아마 리스트로 옮겨보려는 시도중 발생한것으로 추정되며 포기했다
                    inventory.Remove(Item.Code);// 딕셔너리로 만들어져있기에 키값으로 제거해줌 키값을 코드로 넣어서 코드 값으로 지운다
                                                // 허나 리스트 였다면 밸류에서 코드값으로 바꿀수 있었겠지.
                    //inventorylist.Remove(Item);
                    player.Gold += Item.Gold / 2;
                    Console.WriteLine($"{Item.Name}을 판매하여 {Item.Gold / 2}원을 획득하였습니다. ");
                    Console.WriteLine($"{player.Gold}을 보유중 입니다.");
                    //Console.WriteLine("{0}",Item.ItemPlace);
                    Console.WriteLine("");
                    Console.Write(">>확인");
                    Console.Read();
                    Item.ItemPlace = 2;
                    //Console.WriteLine("{0}", Item.ItemPlace);

                }
                else if (player.Gold >= Item.Gold &&  ItemPlace == 2)
                {
                    player.Gold -= Item.Gold;
                    inventory.Add(Item.Code, Item);
                    //inventorylist.Add(Item);

                    Console.WriteLine($"{Item.Name}을 구매하였습니다. ");
                    Console.WriteLine($"{player.Gold}을 보유중 입니다.");
                    Console.WriteLine("");
                    //Console.WriteLine("{0}", Item.ItemPlace);
                    Console.Write(">>확인");
                    Console.Read();
                    Item.ItemPlace = 1;
                    //Console.WriteLine("{0}", Item.ItemPlace);
                }
            }
        }


    }
}