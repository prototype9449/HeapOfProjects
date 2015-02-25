namespace educationProject.GUI
{
    class ForRemove
    {
        //private void GeneralWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    var position = e.GetPosition(this);
        //    if ((position.Y < ElementPanel.Height - 80) && (position.Y > 108))//смотрим на высоту грида с кнопками
        //        FuncGrid.Margin = new Thickness(0, position.Y - 15, 25, 0);
        //}

        //private Image GetTestButton()
        //{
        //    var testButton = new Image();
        //    var bitmap = ImageResources.testButton;
        //    var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
        //        bitmap.GetHbitmap(),
        //        IntPtr.Zero,
        //        Int32Rect.Empty,
        //        BitmapSizeOptions.FromEmptyOptions());
        //    testButton.Source = bitmapSource;
        //    testButton.Width = 129;
        //    testButton.Height = 30;
        //    testButton.HorizontalAlignment = HorizontalAlignment.Right;
        //    testButton.Cursor = allCursors.Hand;
        //    testButton.Margin = new Thickness(0, 0, 40, 0);
        //    //testButton.MouseLeftButtonDown += testButtonMouseClick_Handler;
        //    return testButton;
        //}


        //private Image GetAddNewItemButton()
        //{
        //    var addItemButton = new Image();
        //    var bitmap = ImageResources.AddNewItemButton;
        //    var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
        //        bitmap.GetHbitmap(),
        //        IntPtr.Zero,
        //        Int32Rect.Empty,
        //        BitmapSizeOptions.FromEmptyOptions());
        //    addItemButton.Source = bitmapSource;
        //    addItemButton.Width = 30;
        //    addItemButton.Height = 30;
        //    addItemButton.HorizontalAlignment = HorizontalAlignment.Right;

        //    addItemButton.MouseLeftButtonDown += AddNewItemToLesson_Click;
        //    addItemButton.Cursor = allCursors.Hand;
        //    return addItemButton;
        //}




        //Название дисциплины используется только 1 раз - при создании
        //так же 1 раз создаем кнопки добавления полей и создания теста
        //#region addAddButton;
        //var addNewItemButton = GetAddNewItemButton();
        //Grid.SetRow(addNewItemButton, 4);
        //FuncGrid.Children.Add(addNewItemButton);
        //#endregion;

        //#region addTestButton;
        //var testButton = GetTestButton();
        //Grid.SetRow(testButton, 4);
        //FuncGrid.Children.Add(testButton);
        //#endregion;



        //if (isDrawDisciplineName)
        //    {
        //        #region UserPasswordTextBox;

        //        var authorPasswordTextBox =
        //           new TextBoxWrapper("Придумайте пароль для дальнейшего управления уроком:",
        //                new Thickness(0, 0, 0, 0), 450, 455, 600, HorizontalAlignment.Center,
        //                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDAB9")), 20,
        //                "userPasswortTextBox").GetTextBox();

        //        textBoxes.Add(authorPasswordTextBox);
        //        AddPlaceholderEvent(authorPasswordTextBox);
        //        Grid.SetRow(authorPasswordTextBox, 0);
        //        grid.Children.Add(authorPasswordTextBox);
        //        #endregion;

        //        #region disciplineNameTextBox;
        //        var disciplineNameTextbox = new TextBoxWrapper("Название дисциплины:",
        //                new Thickness(0, 5, 0, 0), 450, 455, 600, HorizontalAlignment.Center).GetTextBox();
        //        textBoxes.Add(disciplineNameTextbox);
        //        AddPlaceholderEvent(disciplineNameTextbox);
        //        Grid.SetRow(disciplineNameTextbox, 1);
        //        grid.Children.Add(disciplineNameTextbox);
        //        #endregion;

                

        //    }
    }
}
