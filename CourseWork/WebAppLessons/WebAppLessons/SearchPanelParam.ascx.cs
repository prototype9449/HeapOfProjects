using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LessonDB;
using LessonDB.LessonDals;

namespace WebAppLessons
{
    public partial class SearchPanelParam : System.Web.UI.UserControl
    {
        public string KindSearch { get; set; }
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ToString();
        private LessonsDal dalLesson;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void ButtonSearch_OnClick(object sender, EventArgs e)
        {
            ListBox.Items.Clear();

            if(KindSearch == "param") dalLesson = new ParamLessonsDal(_connectionString);

            if(KindSearch == "simply") dalLesson = new SimplyLessonsDal(_connectionString);

            if(KindSearch == "story") dalLesson = new StoreProcedureLessonsDal(_connectionString);
            

            var parameters = new List<KeyValuePair<TypeSearch, string>>();

            if (!string.IsNullOrEmpty(TitleText.Text))
            {
                parameters.Add(new KeyValuePair<TypeSearch, string>(TypeSearch.Title, TitleText.Text));
            }
            if (!string.IsNullOrEmpty(AutorText.Text))
            {
                parameters.Add(new KeyValuePair<TypeSearch, string>(TypeSearch.Autor, AutorText.Text));
            }

            if (parameters.Count == 0) return;
            List<LessonInfoId> lessons;


            if (CheckBoxSearch.Checked)
            {
                lessons = dalLesson.GetLessonsByFieldsExactly(parameters);
            }
            else
            {
                lessons = dalLesson.GetLessonsByFieldsLikely(parameters);
            }

            foreach (var lesson in lessons)
            {
                ListBox.Items.Add(lesson.Title + " " + lesson.Autor + " " + lesson.DateCreate);
            }
        }

        protected void ButtonClearResult_OnClick(object sender, EventArgs e)
        {
            ListBox.Items.Clear();
        }
    }
}