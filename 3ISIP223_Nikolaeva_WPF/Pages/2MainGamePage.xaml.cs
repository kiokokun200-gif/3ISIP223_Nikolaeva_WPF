using _3ISIP223_Nikolaeva_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для _2MainGamePage.xaml
    /// </summary>
    public partial class _2MainGamePage : Page
    {
        public Game _game;
        public _2MainGamePage()
        {
            InitializeComponent();
            LoadPicture();

            _game = new Game(this);
            _game.StartGame();
        }

        private void LoadPicture()
        {
            List<BitmapImage> backgroungimages = new List<BitmapImage>()
            {
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca1.jpeg")),
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca2.png")),
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca3.jpg")),
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca4.jpg")),
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca5.jpg"))
            };

            int n  = Raaandom.GetRandomInt(0, backgroungimages.Count - 1);
            ImagBr.ImageSource = backgroungimages[n];
        }

        public void UpdateWeaponImage(Item weapon)
        {
            
            ImgWeapon.Source = new BitmapImage(new Uri(weapon.Image, UriKind.Relative));
            ImgWeapon.ToolTip = $"{weapon.Name.ToUpper()}\nATK: {weapon.Attack}, DEF: {weapon.Defense}";
            ImgWeaponOnPlayer.Source = new BitmapImage(new Uri(weapon.Image, UriKind.Relative));

        }

        public void UpdateArmorImage(Item armor)
        {
           
            ImgArmor.Source = new BitmapImage(new Uri(armor.Image, UriKind.Relative));
            ImgArmor.ToolTip = $"{armor.Name.ToUpper()}\nATK: {armor.Attack}, DEF: {armor.Defense}";
            ImgArmorOnPlayer.Source = new BitmapImage(new Uri(armor.Image, UriKind.Relative));
        }

        public void UpdateHealth(int maxhp, int currenrhp)
        {
            ProgBarHp.Value = currenrhp;
            TxtBlcCurrentHP.Text = $"{currenrhp}/{maxhp}";
        }

        public void UpdateFloor(int floor)
        {
            TxtBlcFloor.Text = $"Этаж: {floor}";
        }
        public class Game
        {
            private _2MainGamePage _page;
            private List<Enemy> _currentEnemies = new List<Enemy>();

            

            public int PlayerHP { get; set; } = 100;
            public int MaxPlayerHP { get; set; } = 100;
            public Item CurrentWeapon { get; set; }
            public Item CurrentArmor { get; set; }

            // Статистика
            public int Turn { get; set; } = 0;
            public bool IsFrozen { get; set; } = false;

            // Фабрика для создания врагов
            private FactoryCreate factoryCreate = new FactoryCreate();

            // Предметы для сундуков
            private List<Item> weapons = new List<Item>
        {
            new Item("Лампа", 10, 0, "/Images/Weapons/Chashka.png"),
            new Item("Волчья погибель", 15, 0, "/Images/Weapons/VolchyaPogibel.png"),
            new Item("Аква Симулякрум", 17, 0, "/Images/Weapons/AquaSimulyacrum.png"),
            new Item("Нефритовый омут", 15, 3, "/Images/Weapons/Omut.png"),
            new Item("Рассекающий туман", 20, 10, "/Images/Weapons/RassekaushiTuman.png")
        };

            private List<Item> armors = new List<Item>
        {
            new Item("Кольчуга", 0, 15, "/Images/Armors/kolchuga.png"),
            new Item("Железная броня", 7, 19, "/Images/Armors/jeleznaya.png"),
            new Item("Золотая броня", 5, 15, "/Images/Armors/zolotaya.png"),
            new Item("Алмазная броня", 5, 20, "/Images/Armors/almaznaya.png")
        };

            

            public Game(_2MainGamePage page)
            {
                _page = page;
                CurrentWeapon = new Item("Дубина переговоров", 7, 0, "/Images/Weapons/dubina.png");
                CurrentArmor = new Item("Кожанная броня", 0, 1, "/Images/Armors/kojanaya.png");
                page.UpdateWeaponImage(CurrentWeapon);
                page.UpdateArmorImage(CurrentArmor);
                page.UpdateHealth(MaxPlayerHP, PlayerHP);

            }

            public void StartGame()
            {
                
                while (PlayerHP > 0)
                {
                    Turn++;
                    _page.UpdateFloor(Turn);
                    


                    // Каждые 10 ходов - босс
                    if (Turn % 10 == 0)
                    {
                        //Console.WriteLine("!!! Появляется БОСС !!!");
                        var boss = GenerateBoss();

                        //Combat(boss);
                    }
                    else
                    {
                        // Случайное событие: 50% враг, 50% сундук
                        if (Raaandom.GetRandomInt(0, 1) == 0)
                        {
                            var enemies = GenerateEnemies();
                            _page.ListMobs.ItemsSource = enemies;
                            Combat(enemies);
                        }
                        else
                        {
                            _page.TxtBlcLogg.Text = "Вы нашли сундук!";
                            _page.ImgChest.Visibility = Visibility.Visible;
                            OpenChest();
                        }
                    }

                    //Console.WriteLine();
                    //Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    //Console.ReadKey();
                    //Console.Clear();
                }

                GameOver();
            }

            private List<Enemy> GenerateEnemies()
            {
                List<Enemy> enemies = new List<Enemy>();

                // Генерируем от 1 до 3 врагов
                int enemyCount = Raaandom.GetRandomInt(1, 4);

                for (int i = 0; i < enemyCount; i++)
                {
                    int randomIndex = Raaandom.GetRandomInt(0, factoryCreate.mob.Count - 1);
                    Factory selectedFactory = factoryCreate.CreateMob(randomIndex);
                    Enemy enemy = selectedFactory.CreateEnemy();
                    enemies.Add(enemy);
                }

                // Выводим информацию о всех врагах
                string enemiesInfo = string.Join(", ", enemies.Select(e => $"{e.Name} (HP: {e.MaxHP})"));
                _page.TxtBlcLogg.Text = $"Появились враги: {enemiesInfo}";

                return enemies;
            }

            private Enemy GenerateBoss()
            {
                int randomIndex = Raaandom.GetRandomInt(0, factoryCreate.boss.Count - 1);
                Factory selectedFactory = factoryCreate.CreateBoss(randomIndex);

                Enemy boss = selectedFactory.CreateEnemy();
                _page.TxtBlcLogg.Text = $"!!! БОСС {boss.Name} !!! (HP: {boss.MaxHP}, Атака: {boss.Attack}, Защита: {boss.Defense})";
                return boss;
            }

            private void Combat(List<Enemy> enemies)
            {
                //_page.TxtBlcLogg.Text = $"Вы встретили: {enemy.Name}";
                ////Console.WriteLine($"Вы встретили: {enemy.Name}");
                //enemy.DisplayInfo();
                //Console.WriteLine();

                //bool playerTurn = true;
                //bool defending = false;

                //while (enemy.CurrentHP > 0 && PlayerHP > 0)
                //{
                //    if (playerTurn)
                //    {
                //        if (IsFrozen)
                //        {
                //            Console.WriteLine("Вы заморожены и пропускаете ход!");
                //            IsFrozen = false;
                //            playerTurn = false;
                //            continue;
                //        }

                //        PlayerTurn(enemy, ref defending);
                //    }
                //    else
                //    {
                //        EnemyTurn(enemy, ref defending);
                //    }

                //    playerTurn = !playerTurn;
                //}

                //if (PlayerHP > 0)
                //{
                //    Console.WriteLine($"Вы победили {enemy.Name}!");
                //}
            }

            private void PlayerTurn(Enemy enemy, ref bool defending)
            {
                Console.WriteLine("Ваш ход:");
                Console.WriteLine("1 - Атаковать");
                Console.WriteLine("2 - Защищаться");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    int damage = CurrentWeapon.Attack;
                    enemy.TakeDamage(damage);
                    Console.WriteLine($"Вы нанесли {damage} урона {enemy.Name}!");
                    defending = false;
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Вы готовитесь к защите...");
                    defending = true;
                }
                else
                {
                    Console.WriteLine("Неверный выбор, вы пропускаете ход!");
                }
            }

            private void EnemyTurn(Enemy enemy, ref bool defending)
            {
                Console.WriteLine($"Ход {enemy.Name}:");

                int damage = enemy.CalculateDamage(CurrentArmor.Defense);
                bool dodged = false;

                // Проверка уклонения при защите
                if (defending && Raaandom.GetRandomDouble() < 0.4)
                {
                    dodged = true;
                    Console.WriteLine("Вы успешно уклонились от атаки!");
                }

                if (!dodged)
                {
                    // Блокирование урона
                    if (defending)
                    {
                        double blockPercent = 0.7 + (Raaandom.GetRandomDouble() * 0.3);
                        int blockedDamage = (int)(damage * blockPercent);
                        damage -= blockedDamage;
                        Console.WriteLine($"Вы заблокировали {blockedDamage} урона!");
                    }

                    PlayerHP -= damage;
                    Console.WriteLine($"{enemy.Name} наносит вам {damage} урона!");

                    // Проверка заморозки
                    if (enemy.TryFreeze())
                    {
                        IsFrozen = true;
                        Console.WriteLine("Враг заморозил вас! Вы пропустите следующий ход.");
                    }
                }

                defending = false;
            }

            private void OpenChest()
            {
                _page.TxtBlcLogg.Text = "Вы нашли сундук!";
                int chestContent = Raaandom.GetRandomInt(0, 2);

                switch (chestContent)
                {
                    case 0: 
                        PlayerHP = MaxPlayerHP;
                        Console.WriteLine("Вы нашли зелье здоровья! Ваше HP полностью восстановлено!");
                        break;

                    case 1: // Оружие
                        Item newWeapon = weapons[Raaandom.GetRandomInt(0, weapons.Count - 1)];
                        Console.WriteLine("Вы нашли новое оружие:");
                        newWeapon.DisplayStats();
                        Console.WriteLine("Ваше текущее оружие:");
                        CurrentWeapon.DisplayStats();
                        OfferItem(newWeapon, true);
                        break;

                    case 2: // Доспехи
                        Item newArmor = armors[Raaandom.GetRandomInt(0, armors.Count - 1)];
                        Console.WriteLine("Вы нашли новые доспехи:");
                        newArmor.DisplayStats();
                        Console.WriteLine("Ваши текущие доспехи:");
                        CurrentArmor.DisplayStats();
                        OfferItem(newArmor, false);
                        break;
                }
            }

            private void OfferItem(Item newItem, bool isWeapon)
            {
                //Console.Write("Хотите взять этот предмет? (д/н): ");
                //string choice = Console.ReadLine().ToLower();

                //if (choice == "д" || choice == "y")
                //{
                //    if (isWeapon)
                //    {
                //        CurrentWeapon = newItem;
                //        Console.WriteLine($"Вы экипировали {newItem.Name}!");
                //    }
                //    else
                //    {
                //        CurrentArmor = newItem;
                //        Console.WriteLine($"Вы экипировали {newItem.Name}!");
                //    }
                //}
                //else
                //{
                //    Console.WriteLine("Вы оставили предмет в сундуке.");
                //}
            }

            private void GameOver()
            {
                //Console.Clear();
                //Console.WriteLine("=== ИГРА ОКОНЧЕНА ===");
                //Console.WriteLine($"Вы продержались {Turn} ходов!");
                //Console.WriteLine("Спасибо за игру!");
            }
        
    }

    }
}
