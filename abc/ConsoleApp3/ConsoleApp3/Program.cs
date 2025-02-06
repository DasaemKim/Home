using System;
using System.Collections.Generic;

class Item
{
    public string Name { get; set; }
    public string Type { get; set; } // 무기나 갑옷
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int HP { get; set; }
    public int Price { get; set; }

    public Item(string name, string type, int attack, int defense, int hp, int price)
    {
        Name = name;
        Type = type;
        Attack = attack;
        Defense = defense;
        HP = hp;
        Price = price;
    }
}

class Character
{
    public string Name { get; set; }
    public string Job { get; set; }
    public int Level { get; set; } = 1;
    public int Attack { get; set; } = 10;
    public int Defense { get; set; } = 5;
    public int HP { get; set; } = 100;
    public int Gold { get; set; } = 1500;

    public List<Item> Inventory { get; set; } = new List<Item>();
    public Item EquippedWeapon { get; set; } = null;
    public Item EquippedArmor { get; set; } = null;

    public Character(string name, string job)
    {
        Name = name;
        Job = job;
    }

    public void ToggleEquipItem(int itemIndex) //아이템 장착 및 해제 기능
    {
        if (itemIndex < 0 || itemIndex >= Inventory.Count)
        {
            Console.WriteLine("잘못된 선택입니다.");
            return;
        }

        Item item = Inventory[itemIndex];

        if ((item.Type == "무기" && EquippedWeapon == item) ||
            (item.Type == "갑옷" && EquippedArmor == item))
        {
            UnequipItem(item); // 이미 장착 중인 아이템이면 해제
            return;
        }

        if (item.Type == "무기") // 기존 장비 해제 후 새로운 아이템 장착
        {
            if (EquippedWeapon != null) Attack -= EquippedWeapon.Attack;
            EquippedWeapon = item;
            Attack += item.Attack;
        }
        else if (item.Type == "갑옷")
        {
            if (EquippedArmor != null)
            {
                Defense -= EquippedArmor.Defense;
                HP -= EquippedArmor.HP;
            }
            EquippedArmor = item;
            Defense += item.Defense;
            HP += item.HP;
        }
        Console.WriteLine($"{item.Name}을(를) 장착했습니다!");
    }

    private void UnequipItem(Item item)
    {
        throw new NotImplementedException();
    }

    public void EquipItem(Item item)
    {
        if (item.Type == "무기")
        {
            if (EquippedWeapon != null)
                Attack -= EquippedWeapon.Attack; // 기존 무기 제거

            EquippedWeapon = item;
            Attack += item.Attack;
        }
        else if (item.Type == "갑옷")
        {
            if (EquippedArmor != null)
            {
                Defense -= EquippedArmor.Defense;
                HP -= EquippedArmor.HP;
            }

            EquippedArmor = item;
            Defense += item.Defense;
            HP += item.HP;
        }
        Console.WriteLine($"{item.Name}을(를) 장착했습니다!");
    }

    public void UnequipItem(string itemType)
    {
        if (itemType == "무기" && EquippedWeapon != null)
        {
            Attack -= EquippedWeapon.Attack;
            Console.WriteLine($"{EquippedWeapon.Name}을(를) 해제했습니다.");
            EquippedWeapon = null;
        }
        else if (itemType == "Armor" && EquippedArmor != null)
        {
            Defense -= EquippedArmor.Defense;
            HP -= EquippedArmor.HP;
            Console.WriteLine($"{EquippedArmor.Name}을(를) 해제했습니다.");
            EquippedArmor = null;
        }
    }

    public void ShowStatus()
    {
        Console.WriteLine("\n[캐릭터 정보]");
        Console.WriteLine($"이름: {Name}, 직업: {Job}, 레벨: {Level}");
        Console.WriteLine($"공격력: {Attack}, 방어력: {Defense}, 체력: {HP}, 골드: {Gold} G");
        Console.WriteLine($"장착 무기: {(EquippedWeapon != null ? EquippedWeapon.Name : "없음")}");
        Console.WriteLine($"장착 방어구: {(EquippedArmor != null ? EquippedArmor.Name : "없음")}");
        Console.WriteLine();
    }

    public void ShowInventory()
    {
        Console.WriteLine("\n[인벤토리]");
        if (Inventory.Count == 0)
        {
            Console.WriteLine("소지한 아이템이 없습니다.");
        }
        else
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                string equipped = "";
                if (EquippedWeapon == Inventory[i] || EquippedArmor == Inventory[i])
                {
                    equipped = "[E]"; // 장착된 아이템 표시
                }
                Console.WriteLine($"{i + 1}. {Inventory[i].Name} {equipped}");
            }
        }
        Console.WriteLine();
    }
}

class Shop
{
    public List<Item> ItemsForSale { get; set; } = new List<Item>();

    public Shop()
    {
        ItemsForSale.Add(new Item("낡은 검", "Weapon", 2, 0, 0, 600));
        ItemsForSale.Add(new Item("청동 도끼", "Weapon", 5, 0, 0, 1500));
        ItemsForSale.Add(new Item("스파르타의 창", "Weapon", 7, 0, 0, 3500));
        ItemsForSale.Add(new Item("수련자 갑옷", "Armor", 0, 5, 20, 1000));
        ItemsForSale.Add(new Item("무쇠갑옷", "Armor", 0, 9, 20, 2000));
        ItemsForSale.Add(new Item("스파르타의 갑옷", "Armor", 0, 15, 20, 3500));
    }

    public void ShowItems()
    {
        Console.WriteLine("\n[상점 아이템 목록]");
        for (int i = 0; i < ItemsForSale.Count; i++)
        {
            var item = ItemsForSale[i];
            Console.WriteLine($"{i + 1}. {item.Name} (공격력: {item.Attack}, 방어력: {item.Defense}, 체력: {item.HP}) - {item.Price} G");
        }
        Console.WriteLine();
    }

    public void BuyItem(Character player, int itemIndex)
    {
        if (itemIndex < 1 || itemIndex > ItemsForSale.Count)
        {
            Console.WriteLine("잘못된 선택입니다.");
            return;
        }

        Item itemToBuy = ItemsForSale[itemIndex - 1];

        foreach (var ownedItem in player.Inventory)
        {
            if (ownedItem.Name == itemToBuy.Name)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                return;
            }
        }

        if (player.Gold >= itemToBuy.Price) // 골드가 충분하면 구매가능
        {
            player.Gold -= itemToBuy.Price;
            player.Inventory.Add(itemToBuy);
            player.EquipItem(itemToBuy); // 아이템 구매 후 자동으로 장착
            Console.WriteLine($"{itemToBuy.Name}을(를) 구매하고 자동으로 장착했습니다!");
        }
        else
        {
            Console.WriteLine("Gold가 부족합니다.");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.Write("캐릭터 이름을 입력하세요: ");
        string name = Console.ReadLine();
        Console.Write("직업을 입력하세요 (전사, 마법사): ");
        string job = Console.ReadLine();

        Character player = new Character(name, job);
        Shop shop = new Shop();

        while (true)
        {
            Console.WriteLine("\n[메뉴]");
            Console.WriteLine("1. 캐릭터 정보 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 장비 장착");
            Console.WriteLine("4. 장비 해제");
            Console.WriteLine("5. 상점");
            Console.WriteLine("6. 종료");
            Console.Write("원하시는 행동을 입력해주세요\n>> ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                player.ShowStatus();
            }
            else if (choice == "2")
            {
                player.ShowInventory();
            }
            else if (choice == "3")
            {
                player.ShowInventory();
                Console.Write("장착할 아이템 번호를 입력하세요: ");
                if (int.TryParse(Console.ReadLine(), out int itemIndex) && itemIndex > 0 && itemIndex <= player.Inventory.Count)
                {
                    player.EquipItem(player.Inventory[itemIndex - 1]);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            else if (choice == "4")
            {
                Console.Write("해제할 장비 유형 (무기/갑옷): ");
                string type = Console.ReadLine();
                player.UnequipItem(type);
            }
            else if (choice == "5")
            {
                shop.ShowItems();
                Console.Write("구매할 아이템 번호를 입력하세요: ");
                if (int.TryParse(Console.ReadLine(), out int shopItemIndex))
                {
                    shop.BuyItem(player, shopItemIndex);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            else if (choice == "6")
            {
                Console.WriteLine("게임을 종료합니다.");
                break;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
