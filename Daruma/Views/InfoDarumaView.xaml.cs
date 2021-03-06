﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using DarumaDAL.WP.Infrastructure;
using Microsoft.Phone.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Daruma.Views
{
    public partial class InfoDarumaView : BaseDarumaPage
    {
        private readonly List<BitmapImage> _tutorStepList;
        private int _currentTutorStep = -1;

        public InfoDarumaView()
        {
            InitializeComponent();
        
            _tutorStepList = new List<BitmapImage>
            {
                new BitmapImage(new Uri(TutorStepUrlRouter.TutorStep1ImageUrl(1), UriKind.Relative)),
                new BitmapImage(new Uri(TutorStepUrlRouter.TutorStep1ImageUrl(2), UriKind.Relative)),
                new BitmapImage(new Uri(TutorStepUrlRouter.TutorStep1ImageUrl(3), UriKind.Relative)),
                new BitmapImage(new Uri(TutorStepUrlRouter.TutorStep1ImageUrl(4), UriKind.Relative)),
                new BitmapImage(new Uri(TutorStepUrlRouter.TutorStep1ImageUrl(5), UriKind.Relative))
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ChangeStep();
        }

        private void Back_OnTap(object sender, EventArgs e)
        {
            _currentTutorStep--;
            ChangeStep();
        }

        private void Forward_OnTap(object sender, EventArgs e)
        {
            Forward();
        }

        private void Forward()
        {
            _currentTutorStep++;
            ChangeStep();
        }

        private void ChangeStep()
        {
            bool isFirstStep = _currentTutorStep < 0;
            //BackButton.Visibility = isFirstStep ? Visibility.Collapsed : Visibility.Visible;

            //hide or show first step text
            InfoText.Visibility = isFirstStep ? Visibility.Visible : Visibility.Collapsed;
            InfoTitle.Visibility = isFirstStep ? Visibility.Visible : Visibility.Collapsed;

            ForwardButton.Visibility = isFirstStep ? Visibility.Visible : Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
            HomeButton.Visibility = Visibility.Collapsed;

            bool isLastStep = _currentTutorStep == _tutorStepList.Count;
            //bool isLastStep = _currentTutorStep == _tutorStepList.Count - 1;
            //ForwardButton.Visibility = isLastStep ? Visibility.Collapsed : Visibility.Visible;
            //HomeButton.Visibility = isLastStep ? Visibility.Visible : Visibility.Collapsed;
            if (isLastStep)
            {
                Home();
                return;
            }

            //set new image for step
            BackgroundImage.ImageSource = isFirstStep ? null : _tutorStepList[_currentTutorStep];
        }

        private void Home_OnTap(object sender, EventArgs e)
        {
            Home();
        }

        private void Home()
        {
            NavigationService.Navigate(new Uri(ViewUrlRouter.MainViewUrl, UriKind.Relative));
        }

        private void Gird_OnTap(object sender, GestureEventArgs e)
        {
            IfNotFirstSetp();
        }

        private void GestureListener_OnFlick(object sender, FlickGestureEventArgs e)
        {
            IfNotFirstSetp();
        }

        private void GestureListener_OnHold(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            IfNotFirstSetp();
        }

        private void IfNotFirstSetp()
        {
            if (_currentTutorStep >= 0)
            {
                Forward();
            }
        }
    }
}