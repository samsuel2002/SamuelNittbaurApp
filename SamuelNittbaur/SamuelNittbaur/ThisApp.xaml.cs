using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Npgsql;
using Microcharts;
using System.Reflection;
using Microcharts.Forms;
using SkiaSharp;
using Xamarin.Essentials;

namespace SamuelNittbaur
{
    public partial class ThisApp : ContentPage
    {
        List<TextBlockPrefab> textBlockPrefabs = new List<TextBlockPrefab>();
        public ThisApp()
        {
            InitializeComponent();
            //DeleteTable();
            //CreateDB();
            //InsertData();
            GetFromDB();
            Add_Feedback();
            Add_Chart();
        }

        private void Add_Feedback()
        {

            Frame frame = new Frame()
            {
                Margin = new Thickness(10),
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#a8a8a8")
            };
            StackLayout layout = new StackLayout();
            Label label = new Label()
            {
                Text = "Wie findest du die Idee dieser App?",
                FontAttributes = FontAttributes.Bold,
                FontSize = 15
            };
            StackLayout button_layout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal
            };
            Button positive_button = new Button()
            {
                Text = "Mega, echt cool",
                BackgroundColor = Color.FromHex("#00b831")
            };
            Button negative_button = new Button()
            {
                Text = "Nicht so mega",
                BackgroundColor = Color.FromHex("#ad1515")
            };

            button_layout.Children.Add(positive_button);
            button_layout.Children.Add(negative_button);



            positive_button.Clicked += async (sender, args) =>
            {
                await InsertFeedback("positive");
                App.Current.MainPage = new ThisApp();
            };
            negative_button.Clicked += async (sender, args) =>
            {
                await InsertFeedback("negative");
                App.Current.MainPage = new ThisApp();
            };



            layout.Children.Add(label);
            layout.Children.Add(button_layout);
            frame.Content = layout;

            ContentFrame.Children.Add(frame);
        }

        

        private void Add_Chart()
        {
            var entries = new[]
           {
            new ChartEntry(Get_Feedback_Count("positive"))
            {
                Label = "Positive",
                Color = SKColor.Parse("#00b831")
            },
            new ChartEntry(Get_Feedback_Count("negative"))
            {
                Label = "Negative",
                Color = SKColor.Parse("#ad1515")
            }

            };

            var chart = new PieChart() { Entries = entries };
            chart.BackgroundColor = SKColor.Empty; 
            
            var chartView = new Microcharts.Forms.ChartView { Chart = chart };
            chartView.HeightRequest = 300; 

            ContentFrame.Children.Add(chartView);
        }


        private void Back_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new OverView();
        }

       
        private async void InsertData()
        {
             await InsertIntoDB(new TextBlockPrefab()
            {
                Content = "Info",
                Header = true
            });
            await Task.Delay(1000);

            await InsertIntoDB(new TextBlockPrefab()
            {
                Content = "Die Idee dieser App besteht aus einer orginellen Idee, um meine Persönlichkeit zu zeigen. Die App wird dabei zur Einsicht auch öffentlich in meinem Github Repository zur Verfügung stehen. Jedoch gibt es bei der Idee ein Problem: Die App hat nicht besonders viel Funktionalität und in diesem Kontext gibt es auch nicht besonder viel, was man dabei Backend-technisch einfügen kann. Jedoch habe ich mir ein paar Punkte überlegt, die im folgenden aufgelistet werden:",
                Header = false
            });
            await Task.Delay(1000);

            await InsertIntoDB(new TextBlockPrefab()
            {
                Content = "Laden aus einer Datenbank",
                Header = true
            });
            await Task.Delay(1000);

            await InsertIntoDB(new TextBlockPrefab()
            {
                Content = "Um meine Fähigkeiten im Bereich SQL und Datenbanken zu beweisen, sind die Texte auf dieser Seite aus einer Online Datenbank entzogen.",
                Header = false
            }); 
            await Task.Delay(1000);

            await InsertIntoDB(new TextBlockPrefab()
            {
                Content = "FeedBack",
                Header = true
            });
            await Task.Delay(1000);

            await InsertIntoDB(new TextBlockPrefab()
            {
                Content = "Das oben gezeigte Formularfeld wird dynmaisch über C# erzeugt, und die Daten werden in dem Diagramm ausgegeben. Die Daten werden ebenfalls in der Datenbank gespeichert.",
                Header = false
            });
            await Task.Delay(1000);
        }

        private async void CreateDB()
        {
            await using var dataSource = NpgsqlDataSource.Create(GetConnectionString());

            // Insert some data
            await using (var cmd = dataSource.CreateCommand("CREATE TABLE IF NOT EXISTS TextRelation (ContentID integer NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY, Content varchar(1000), Header bool)")) 
            {
                await cmd.ExecuteNonQueryAsync();
            } 
            await using (var cmd = dataSource.CreateCommand("CREATE TABLE IF NOT EXISTS FeedbackTable (FeedbackID integer NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY, PositiveOrNegative varchar(1000))")) 
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private async void DeleteTable()
        {

            await using var dataSource = NpgsqlDataSource.Create(GetConnectionString());

            // Insert some data
            await using (var cmd = dataSource.CreateCommand("DROP TABLE TextRelation"))
            {
                await cmd.ExecuteNonQueryAsync();
            }

            await using (var cmd = dataSource.CreateCommand("DROP TABLE FeedbackTable"))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }
        private async Task InsertFeedback(string feedback)
        {
            using var dataSource = NpgsqlDataSource.Create(GetConnectionString());

            await using (var cmd = dataSource.CreateCommand("INSERT INTO FeedbackTable(PositiveOrNegative) Values($1)"))
            {
                cmd.Parameters.AddWithValue(feedback);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private async Task InsertIntoDB(TextBlockPrefab textToAdd)
        {
            using var dataSource = NpgsqlDataSource.Create(GetConnectionString());

            await using (var cmd = dataSource.CreateCommand("INSERT INTO TextRelation(Content,Header) Values($1,$2)"))
            {
                cmd.Parameters.AddWithValue(textToAdd.Content);
                cmd.Parameters.AddWithValue(textToAdd.Header);
                await cmd.ExecuteNonQueryAsync();
            }
            await Task.Delay(200);
            
        }

        private async void GetFromDB()
        {
            using var dataSource = NpgsqlDataSource.Create(GetConnectionString());
            using (var cmd = dataSource.CreateCommand("SELECT * FROM TextRelation"))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    TextBlockPrefab textToAdd = new TextBlockPrefab()
                    {
                        ID = reader.GetInt32(0),
                        Content = reader.GetString(1),
                        Header = reader.GetBoolean(2)
                    };

                    textBlockPrefabs.Add(textToAdd);
                }
            }


            DynamiclyCreateLabels();
        }

        private int Get_Feedback_Count(string entry_value)
        {
            int return_value = 0;
            using var dataSource = NpgsqlDataSource.Create(GetConnectionString());
            using (var cmd = dataSource.CreateCommand("SELECT COUNT(*) FROM FeedbackTable WHERE PositiveOrNegative='" + entry_value +"'"))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                   return_value = reader.GetInt32(0);
                }
            }

            return return_value;
        }

        private void DynamiclyCreateLabels()
        {
            textBlockPrefabs.Sort((paramOne, paramTwo) => paramOne.ID.CompareTo(paramTwo.ID));

            foreach (var text in textBlockPrefabs)
            {
                Label lblToAdd = new Label()
                {
                    Text = text.Content,
                    Margin = new Thickness(5),
                    TextColor = Color.White
                };
                if (text.Header)
                {
                    lblToAdd.FontAttributes = FontAttributes.Bold;
                    lblToAdd.FontSize = 20;
                }

                ContentFrame.Children.Add(lblToAdd);
            }
        }


        private string GetConnectionString()
        {
            var connStringBuilder = new NpgsqlConnectionStringBuilder()
            {
                Host = "samuelnnittbaurapp-8728.8nj.cockroachlabs.cloud",
                Port = 26257,
                SslMode = SslMode.Require,
                Username = "samuel",
                Password = "OUmeOzMtt-jEDA9DOpDo_g",
                Database = "defaultdb",
                Options = "--cluster=samuelnnittbaurapp-8728",
                RootCertificate = "~/.postgres/root.crt",
                TrustServerCertificate = true
            };

            return connStringBuilder.ConnectionString;

        }

      
    }

    public class TextBlockPrefab
    {
        public string Content { get; set; }

        public bool Header { get; set; }

        public int ID { get; set; }
    }
   
}
