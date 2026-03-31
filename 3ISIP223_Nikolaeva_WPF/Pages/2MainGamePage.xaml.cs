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
    public partial class _2MainGamePage : Page
    {
        public Game _game;
        private bool _waitingForTarget = false;

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
                new BitmapImage(new Uri("pack://application:,,,/Images/Backgrounds/Loca1.jpeg")),
                new BitmapImage(new Uri("pack://application:,,,/Images/Backgrounds/Loca2.png")),
                new BitmapImage(new Uri("pack://application:,,,/Images/Backgrounds/Loca3.jpg")),
                new BitmapImage(new Uri("pack://application:,,,/Images/Backgrounds/Loca4.jpg")),
                new BitmapImage(new Uri("pack://application:,,,/Images/Backgrounds/Loca5.jpg"))
            };

            int n = Raaandom.GetRandomInt(0, backgroungimages.Count - 1);
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

        public void SetLog(string message)
        {
            TxtBlcLogg.Text = message;
        }

        public void ShowEnemies(List<Enemy> enemies)
        {
            ListMobs.ItemsSource = enemies;
        }

        public void UpdateEnemyList()
        {
            ListMobs.ItemsSource = null;
            ListMobs.ItemsSource = _game.CurrentEnemies;
        }

        public void EnableCombatMode(bool enable)
        {
            BtnAttack.IsEnabled = enable;
            BtnDefence.IsEnabled = enable;
            if (enable)
            {
                TxtBlcHint.Visibility = Visibility.Collapsed;
            }
        }

        public void WaitForTargetSelection()
        {
            _waitingForTarget = true;
            TxtBlcHint.Visibility = Visibility.Visible;
            TxtBlcHint.Text = "👉 КЛИКНИТЕ ПО ВРАГУ ДЛЯ АТАКИ 👈";
        }

        public void CancelTargetSelection()
        {
            _waitingForTarget = false;
            TxtBlcHint.Visibility = Visibility.Collapsed;
        }

        private void BtnAttack_Click(object sender, RoutedEventArgs e)
        {
            _game.PlayerAttack();
        }

        private void BtnDefence_Click(object sender, RoutedEventArgs e)
        {
            _game.PlayerDefend();
        }

        private void ListMobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, ждем ли выбора цели
            if (_waitingForTarget && ListMobs.SelectedItem is Enemy selected)
            {
                _waitingForTarget = false;
                TxtBlcHint.Visibility = Visibility.Collapsed;
                _game.OnEnemySelected(selected);
                ListMobs.SelectedItem = null; // Снимаем выделение
            }
        }

        public class Game
        {
            private _2MainGamePage _page;
            private List<Enemy> _currentEnemies = new List<Enemy>();
            private bool _isInCombat = false;
            private bool _isDefending = false;
            private Enemy _currentBoss = null;

            public int PlayerHP { get; set; } = 100;
            public int MaxPlayerHP { get; set; } = 100;
            public Item CurrentWeapon { get; set; }
            public Item CurrentArmor { get; set; }
            public int Turn { get; set; } = 0;
            public bool IsFrozen { get; set; } = false;
            public List<Enemy> CurrentEnemies => _currentEnemies;

            private FactoryCreate factoryCreate = new FactoryCreate();

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
                _page.UpdateWeaponImage(CurrentWeapon);
                _page.UpdateArmorImage(CurrentArmor);
                _page.UpdateHealth(MaxPlayerHP, PlayerHP);
            }

            public async void StartGame()
            {
                await NextTurn();
            }

            private async System.Threading.Tasks.Task NextTurn()
            {
                if (PlayerHP <= 0)
                {
                    GameOver();
                    return;
                }

                Turn++;
                _page.UpdateFloor(Turn);

                if (Turn % 10 == 0)
                {
                    _currentBoss = GenerateBoss();
                    _currentEnemies = new List<Enemy> { _currentBoss };
                    _page.ShowEnemies(_currentEnemies);
                    _page.SetLog($"!!! БОСС {_currentBoss.Name} появился !!!");
                    StartCombat();
                }
                else
                {
                    if (Raaandom.GetRandomInt(0, 1) == 0)
                    {
                        _currentEnemies = GenerateEnemies();
                        _page.ShowEnemies(_currentEnemies);
                        _page.SetLog($"Появились враги: {string.Join(", ", _currentEnemies.Select(e => e.Name))}");
                        StartCombat();
                    }
                    else
                    {
                        OpenChest();
                    }
                }
            }

            private List<Enemy> GenerateEnemies()
            {
                List<Enemy> enemies = new List<Enemy>();

                // Выбираем тип врага
                int enemyTypeIndex = Raaandom.GetRandomInt(0, factoryCreate.mob.Count - 1);
                Factory selectedFactory = factoryCreate.CreateMob(enemyTypeIndex);

                // Количество врагов от 1 до 3
                int enemyCount = Raaandom.GetRandomInt(1, 4);

                // Создаем врагов одного типа
                for (int i = 0; i < enemyCount; i++)
                {
                    Enemy enemy = selectedFactory.CreateEnemy();
                    enemies.Add(enemy);
                }

                // Выводим сообщение
                _page.SetLog($"Появились {enemies[0].Name} x{enemyCount}!");

                return enemies;
            }

            private Enemy GenerateBoss()
            {
                int randomIndex = Raaandom.GetRandomInt(0, factoryCreate.boss.Count - 1);
                Factory selectedFactory = factoryCreate.CreateBoss(randomIndex);
                return selectedFactory.CreateEnemy();
            }

            private void StartCombat()
            {
                _isInCombat = true;
                _isDefending = false;
                _page.EnableCombatMode(true);

                // Если игрок заморожен, сразу ход врага
                if (IsFrozen)
                {
                    _page.SetLog("Вы заморожены! Враг атакует...");
                    IsFrozen = false;
                    EnemyTurn();
                }
            }

            public void PlayerAttack()
            {
                if (!_isInCombat) return;
                if (_currentEnemies.Count == 0)
                {
                    EndCombat();
                    return;
                }

                _page.SetLog("Выберите цель для атаки...");
                _page.WaitForTargetSelection();
            }

            public void OnEnemySelected(Enemy target)
            {
                if (!_isInCombat) return;

                // Атака
                int damage = CurrentWeapon.Attack;
                target.TakeDamage(damage);
                _page.SetLog($"Вы нанесли {damage} урона {target.Name}!");

                // Проверка смерти
                if (target.CurrentHP <= 0)
                {
                    _currentEnemies.Remove(target);
                    _page.UpdateEnemyList();
                    _page.SetLog($"{target.Name} повержен!");

                    if (_currentEnemies.Count == 0)
                    {
                        EndCombat();
                        return;
                    }
                }
                else
                {
                    _page.UpdateEnemyList();
                }

                // Ход врага
                EnemyTurn();
            }

            public void PlayerDefend()
            {
                if (!_isInCombat) return;

                _isDefending = true;
                _page.SetLog("Вы встали в защитную стойку!");

                // Ход врага
                EnemyTurn();
            }

            private async void EnemyTurn()
            {
                if (!_isInCombat) return;

                foreach (var enemy in _currentEnemies.Where(e => e.CurrentHP > 0).ToList())
                {
                    if (PlayerHP <= 0) break;

                    // Меняем картинку на атаку
                    // _page.SetEnemyImage(enemy.AttackImage);

                    _page.SetLog($"Ход {enemy.Name}:");

                    int damage = enemy.CalculateDamage(CurrentArmor.Defense);
                    bool dodged = false;

                    // Уклонение
                    if (_isDefending && Raaandom.GetRandomDouble() < 0.4)
                    {
                        dodged = true;
                        _page.SetLog("Вы уклонились от атаки!");
                    }

                    if (!dodged)
                    {
                        // Блок
                        if (_isDefending)
                        {
                            double blockPercent = 0.7 + (Raaandom.GetRandomDouble() * 0.3);
                            int blockedDamage = (int)(damage * blockPercent);
                            damage -= blockedDamage;
                            _page.SetLog($"Вы заблокировали {blockedDamage} урона!");
                        }

                        PlayerHP -= damage;
                        _page.UpdateHealth(MaxPlayerHP, PlayerHP);
                        _page.SetLog($"{enemy.Name} наносит {damage} урона!");

                        if (PlayerHP <= 0)
                        {
                            _page.SetLog("ВЫ ПОГИБЛИ!");
                            GameOver();
                            return;
                        }

                        // Заморозка
                        if (enemy.TryFreeze())
                        {
                            IsFrozen = true;
                            _page.SetLog("Вы заморожены!");
                        }
                    }

                    await System.Threading.Tasks.Task.Delay(500);
                }

                _isDefending = false;

                // Возвращаем обычную картинку
                // foreach (var enemy in _currentEnemies) _page.SetEnemyImage(enemy.DefaultImage);
            }

            private async void EndCombat()
            {
                _isInCombat = false;
                _page.EnableCombatMode(false);
                _page.SetLog("Победа!");

                await System.Threading.Tasks.Task.Delay(1000);

                // Продолжаем игру
                await NextTurn();
            }

            private void OpenChest()
            {
                _page.SetLog("Вы нашли сундук!");
                _page.ImgChest.Visibility = Visibility.Visible;

                int chestContent = Raaandom.GetRandomInt(0, 2);

                switch (chestContent)
                {
                    case 0:
                        PlayerHP = MaxPlayerHP;
                        _page.UpdateHealth(MaxPlayerHP, PlayerHP);
                        _page.SetLog("Зелье здоровья! HP восстановлено!");
                        break;

                    case 1:
                        Item newWeapon = weapons[Raaandom.GetRandomInt(0, weapons.Count - 1)];
                        _page.SetLog($"Найдено оружие: {newWeapon.Name}!");
                        ShowItemChoice(newWeapon, true);
                        return;

                    case 2:
                        Item newArmor = armors[Raaandom.GetRandomInt(0, armors.Count - 1)];
                        _page.SetLog($"Найдены доспехи: {newArmor.Name}!");
                        ShowItemChoice(newArmor, false);
                        return;
                }

                _page.ImgChest.Visibility = Visibility.Hidden;
                NextTurn();
            }

            private async void ShowItemChoice(Item newItem, bool isWeapon)
            {
                string currentName = isWeapon ? CurrentWeapon.Name : CurrentArmor.Name;
                int currentStat = isWeapon ? CurrentWeapon.Attack : CurrentArmor.Defense;
                int newStat = isWeapon ? newItem.Attack : newItem.Defense;

                var result = MessageBox.Show(
                    $"Найдено: {newItem.Name} ({(isWeapon ? "Атака" : "Защита")}: {newStat})\n\n" +
                    $"Текущее: {currentName} ({(isWeapon ? "Атака" : "Защита")}: {currentStat})\n\n" +
                    "Заменить?",
                    "Сундук",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (isWeapon)
                    {
                        CurrentWeapon = newItem;
                        _page.UpdateWeaponImage(newItem);
                    }
                    else
                    {
                        CurrentArmor = newItem;
                        _page.UpdateArmorImage(newItem);
                    }
                    _page.SetLog($"Вы экипировали {newItem.Name}!");
                }
                else
                {
                    _page.SetLog($"Вы оставили {newItem.Name} в сундуке.");
                }

                _page.ImgChest.Visibility = Visibility.Hidden;
                await System.Threading.Tasks.Task.Delay(500);
                NextTurn();
            }

            private void GameOver()
            {
                _page.SetLog($"ИГРА ОКОНЧЕНА! Вы продержались {Turn} ходов.");
                _page.EnableCombatMode(false);
                MessageBox.Show($"Игра окончена!\nВы продержались {Turn} ходов.", "Game Over",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Возврат в меню
                _page.NavigationService.Navigate(new _1StartPage());
            }
        }
    }
}
