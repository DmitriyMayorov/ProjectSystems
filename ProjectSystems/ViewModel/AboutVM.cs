using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSystems.ViewModel
{
    public class AboutVM : ViewModelBase
    {
        private string _aboutText;
        public string AboutText
        {
            get { return _aboutText; }
            set { _aboutText = value; OnPropertyChanged(); }
        }

        public ICommand AboutProgramCommand { get; set; }
        public ICommand TaskInfoCommand { get; set; }  
        public ICommand AddTaskCommand { get; set; }   
        public ICommand UpdateAndDeleteTaskCommand { get; set; }
        public ICommand InfSectionInfoCommand { get; set; }
        public ICommand AddAndDeleteInfSectionCommand { get; set; }
        public ICommand PageInfoCommand { get; set; }
        public ICommand AddAndDeletePageCommand { get; set; }
        public ICommand TrackTimeInfoCommand { get; set; }
        public ICommand ReportTrackTimeInfoCommad { get; set; }
        public ICommand ReportStateTaskForCurrentProjectInfoCommand { get; set; }
        public ICommand WorkersGridProjectCommand { get; set; }
        public ICommand ProjectsGridCommand { get; set; }

        private void AboutProgramExecute(object obj)
        {
            AboutText = "Система упарвления проектами предназанчена для четкой структуризации и мониторинга проектов. Даёт возможность составления статистики по проектам и работникам";
        }

        private void TaskInfoExecute(object obj) 
        {
            AboutText = "Задания предсталвены в виде таблицы во вкладке проектов главного экарана приложения";
        }

        private void AddTaskExecute(object obj)
        {
            AboutText = "Добавление заданий производится через специальную форму, нажав на кнопку добавление '+' в верхнем меню";
        }

        private void UpdateAndDeleteTaskExecute(object obj) 
        {
            AboutText = "Удаление заданий проивзодится за счёт выделение в таблице той строки-проекта, который преднезначен для удаления. Редактирование производится напрямую с данными таблицы";
            AboutText += "Также следует нажать на соответствующие интуитивно понятные кнопки в верхнем меню для применения изменений";
        }

        private void InfSectionInfoExecute(object obj)
        {
            AboutText = "Информационные секции - разделы хранения нужной для работников и для проектов информации\n Секции содержат страницы информационых блоков пользователей";
        }

        private void AddAndDeleteInfSectionExecute(object obj)
        {
            AboutText = "Добавление инфомарционных секций производится через специальную форму, нажав на кнопку добавление '+' в верхнем меню\n";
            AboutText += "Удаление же проивзодится путём выделения нужной секции и нажатия соответствующей кнопки в верхнем меню\n";
        }

        private void PageInfoExecute(object obj)
        {
            AboutText = "Страницы - это раздлы с конкретной информацией по конкретной теме, хранятся в ифномарционных секциях";
        }

        private void AddAndDeletePageExecute(object obj)
        {
            AboutText = "Добавление проивзодится через специальную форму по средствам нажатия соответствующей кнопки в верхнем меню\n";
            AboutText += "Удаление проивзодится по средствам выделения нужной страницы в меню и её удаление по средствам нажатия соответствующей кнопки в верхнем меню";
        }

        private void TrackTimeInfoExecute(object obj)
        {
            AboutText = "Трек времени - фиксация факта работы над заданием каждого ключевого сотрудника. Каждый добавляет количество часов - сколько данный работник за 1(!) день " +
                "проработал на данным заданием";
        }

        private void ReportTrackTimeInfoExecute(object obj)
        {
            AboutText = "Вывод статистики по каждому работнику за конкретный период времени (сколько выполнил заданий, сколько часов зафиксировал)";
        }

        private void ReportStateTaskForCurrentProjectInfoExecute(object obj)
        {
            AboutText = "Вывод статистики о всех заданиях для конкретного проекта и то, на каких этапах готвоности данные задания находятся";
        }

        private void WorkersGridProjectExecute(object obj)
        {
            AboutText = "Вывод инфомации о каждом из работников в виде таблицы";
        }

        private void ProjectsGridExecute(object obj)
        {
            AboutText = "Вывод информации о всех проектах в виде таблицы";
        }

        public AboutVM() 
        {
            AboutProgramCommand = new RelayCommand(AboutProgramExecute);
            TaskInfoCommand = new RelayCommand(TaskInfoExecute);
            AddTaskCommand = new RelayCommand(AddTaskExecute);
            UpdateAndDeleteTaskCommand = new RelayCommand(UpdateAndDeleteTaskExecute);
            InfSectionInfoCommand = new RelayCommand(InfSectionInfoExecute);
            AddAndDeleteInfSectionCommand = new RelayCommand(AddAndDeleteInfSectionExecute);
            PageInfoCommand = new RelayCommand(PageInfoExecute);
            AddAndDeletePageCommand = new RelayCommand(AddAndDeletePageExecute);
            TrackTimeInfoCommand = new RelayCommand(TrackTimeInfoExecute);
            ReportTrackTimeInfoCommad = new RelayCommand(ReportTrackTimeInfoExecute);
            ReportStateTaskForCurrentProjectInfoCommand = new RelayCommand(ReportStateTaskForCurrentProjectInfoExecute);
            WorkersGridProjectCommand = new RelayCommand(WorkersGridProjectExecute);
            ProjectsGridCommand = new RelayCommand(ProjectsGridExecute);
        }
    }
}
