using ResSched.Models;

using Xamarin.Forms;

namespace ResSched.Controls
{
    public class HourlyViewCell : ContentView
    {
        public static readonly BindableProperty HourlyScheduleProperty = BindableProperty.Create(propertyName: "HourlySchedule",
               returnType: typeof(HourlySchedule),
               declaringType: typeof(HourlyViewCell),
               propertyChanged: OnPropertyChanged,
               defaultValue: default(HourlySchedule));

        public HourlyViewCell()
        {
            Init();
        }

        public HourlySchedule HourlySchedule
        {
            get { return (HourlySchedule)GetValue(HourlyScheduleProperty); }
            set { SetValue(HourlyScheduleProperty, value); }
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var viewCell = (HourlyViewCell)bindable;
            viewCell.Init();
        }

        private void Init()
        {
            /**
             * <!--<Grid Margin="40,0,0,0">
                                <Grid.ColumnDefinitions>
                                    <ColumnDefinition Width="auto" />
                                    <ColumnDefinition Width="1*" />
                                    <ColumnDefinition Width="2*" />
                                    <ColumnDefinition Width="auto" />
                                </Grid.ColumnDefinitions>
                                <Label Grid.Column="0" Text="{Binding CalendarIcon}" VerticalOptions="Center" Margin="5" FontSize="Medium"
                                       TextColor="{Binding IsReserved, Converter={StaticResource BoolToCalendarIconColorConverter}}" Style="{DynamicResource FA.Regular.LabelStyle}" />
                                <Label Grid.Column="1" Text="{Binding HourDisplay}" HorizontalOptions="Start" VerticalOptions="Center" />
                                <Label Grid.Column="2" Text="{Binding ReservedMessage}" HorizontalOptions="Start" VerticalOptions="Center" Margin="5"
                                       TextColor="{Binding IsReserved, Converter={StaticResource BoolToReservationColorConverter}}" />
                            </Grid>-->
            **/
            Grid g = new Grid();
            g.Margin = new Thickness(40, 0, 0, 0);
            ColumnDefinition cd1 = new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) };
            ColumnDefinition cd2 = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
            ColumnDefinition cd3 = new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) };
            ColumnDefinition cd4 = new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) };
            g.ColumnDefinitions.Add(cd1);
            g.ColumnDefinitions.Add(cd2);
            g.ColumnDefinitions.Add(cd3);
            g.ColumnDefinitions.Add(cd4);

            Label label1 = new Label();
            label1.Text = HourlySchedule.CalendarIcon;
            label1.VerticalOptions = LayoutOptions.Center;
            label1.Margin = 5;
        }
    }
}