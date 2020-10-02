using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.Entity;
using System.Globalization;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;

namespace Valeria
{
    public partial class Form1 : Form
    {
        private CultureInfo Ci = new CultureInfo("en-us");
        private SpeechSynthesizer Ss = new SpeechSynthesizer();
        private SpeechRecognitionEngine Sre;
        private List<string> Questions = new List<string>();
        private string[] Answers = new string[13];
        private Random rnd = new Random();
        private int currentQuestionId = -1;
        private Thread QuestioningThread;
        private bool questioningStarted = false;
        private bool nextQuestion;
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(350, 500);
            QuestioningThread = new Thread(Questioning);

            inputFace.Size = new Size(200, 200);
            inputFace.Location = new Point(75, 150);

            LoadQuestions();

            ValeriaDataBase vdb = new ValeriaDataBase();
            vdb.Days.Add(new DayInfo { Day = DateTime.Now, Q1 = 0, Q2 = 0, Q3 = 0, Q4 = 0, Q5 = 0, Q6 = 0, Q7 = 0, Q8 = 0, Q9 = 0, Q10 = 0, Q11 = 0, Q12 = 0, Q13 = 0, Total = 0 });
            vdb.SaveChanges();

            Ss.SetOutputToDefaultAudioDevice();

            Sre = new SpeechRecognitionEngine(Ci);
            Sre.SetInputToDefaultAudioDevice();
            Sre.SpeechRecognized += Sre_SpeechRecognized;
            LoadGreetingGrammar();
            LoadAnswersGrammar();
            Sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void LoadGreetingGrammar()
        {
            Choices GreetingVariants = new Choices();
            GreetingVariants.Add("Hello");
            GreetingVariants.Add("Hi");
            GreetingVariants.Add("Good morning");
            GreetingVariants.Add("Good afternoon");
            GreetingVariants.Add("Good evening");

            GrammarBuilder GreetingGb = new GrammarBuilder();
            GreetingGb.Culture = Ci;
            GreetingGb.Append(GreetingVariants);

            Grammar GreetingGrm = new Grammar(GreetingGb);
            Sre.LoadGrammar(GreetingGrm);
        }

        private void LoadAnswersGrammar()
        {
            Choices AnswerVariants = new Choices();
            AnswerVariants.Add("Yes");
            AnswerVariants.Add("Yeah");
            AnswerVariants.Add("Yep");
            AnswerVariants.Add("No");
            AnswerVariants.Add("Not");
            AnswerVariants.Add("A bit");
            AnswerVariants.Add("A little");
            AnswerVariants.Add("So so");

            GrammarBuilder AnswersGb = new GrammarBuilder();
            AnswersGb.Culture = Ci;
            AnswersGb.Append(AnswerVariants);

            Grammar AnswersGrm = new Grammar(AnswersGb);
            Sre.LoadGrammar(AnswersGrm);
        }

        private void Questioning()
        {
            Thread.Sleep(3000);
            for (int i = 0; i < 13; i++)
            {
                Ss.Speak(Questions[i]);
                currentQuestionId++;
                nextQuestion = false;
                for (;;)
                {
                    if (nextQuestion == true)
                        break;
                }
            }
            AnswersAnalysing();
        }

        private void AnswersAnalysing()
        {
            DayInfo cdi = new DayInfo();
            cdi.Day = DateTime.Now;
            int Qmark;

            //Question 1
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = 2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = -2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = 1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q1 = Qmark;

            //Question 2
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = 2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = -2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = 1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q2 = Qmark;

            //Question 3
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = -2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = 2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = -1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q3 = Qmark;

            //Question 4
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = 2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = -2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = 1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q4 = Qmark;

            //Question 5
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = -2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = 2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = -1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q5 = Qmark;

            //Question 6
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = -2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = 2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = -1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q6 = Qmark;

            //Question 7
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = 2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = -2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = 1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q7 = Qmark;

            //Question 8
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = -2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = 2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = -1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q8 = Qmark;

            //Question 9
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = 2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = -2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = 1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q9 = Qmark;

            //Question 10
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = -2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = 2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = -1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q10 = Qmark;

            //Question 11
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = 2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = -2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = 1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q11 = Qmark;

            //Question 12
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = 2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = -2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = 1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q12 = Qmark;

            //Question 13
            Qmark = 0;
            if (Answers[0] == "Yes" || Answers[0] == "Yeah" || Answers[0] == "Yep")
                Qmark = -2;
            else if (Answers[0] == "No" || Answers[0] == "Not")
                Qmark = 2;
            else if (Answers[0] == "A bit" || Answers[0] == "A little")
                Qmark = -1;
            else if (Answers[0] == "So so")
                Qmark = 0;
            cdi.Q13 = Qmark;

            int total = cdi.Q1 + cdi.Q2 + cdi.Q3 + cdi.Q4 + cdi.Q5 + cdi.Q6 + cdi.Q7 + cdi.Q8 + cdi.Q9 + cdi.Q10 + cdi.Q11 + cdi.Q12 + cdi.Q13;
            cdi.Total = total;

            ValeriaDataBase vdb = new ValeriaDataBase();
            vdb.Days.Add(cdi);
            vdb.SaveChanges();
        }

        private void LoadQuestions()
        {
            Questions.Add("Is everything alright?");
            Questions.Add("Did you sleep well?");
            Questions.Add("Are you nervous right now?");
            Questions.Add("Is everything under control?");
            Questions.Add("Is your mood bad right now?");
            Questions.Add("Was you tired at morning?");
            Questions.Add("Are you happy?");
            Questions.Add("Are you tired right now?");
            Questions.Add("Did you exercised some sport today?");
            Questions.Add("Did you have a headache this morning?");
            Questions.Add("Is everything in success?");
            Questions.Add("Did you have morning exercises today?");
            Questions.Add("Do you have a headache right now?");
        }

        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if ((e.Result.Text.IndexOf("Hello") >= 0 || e.Result.Text.IndexOf("Hi") >= 0 || e.Result.Text.IndexOf("Good morning") >= 0 || e.Result.Text.IndexOf("Good afternoon") >= 0 || e.Result.Text.IndexOf("Good evening") >= 0) && questioningStarted == false)
            {
                Ss.Speak(e.Result.Text);
                questioningStarted = true;
                QuestioningThread.Start();
            }
            else
            {
                Answers[currentQuestionId] = e.Result.Text;
                nextQuestion = true;
            }
        }
    }
}
