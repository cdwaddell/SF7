using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.Media.SpeechRecognition;
using Windows.UI.Xaml.Controls;

namespace com.cdwaddell.sf7
{
    public sealed partial class MainPage : Page
    {
        private Robot _robot;
        public MainPage()
        {
            InitializeComponent();
            InitializeSpeechRecognizer();
            InitializeMotors();
        }

        private void InitializeMotors()
        {
            _robot = new Robot();
        }
        
        /// <summary>
        /// Initializes MyRecognizer and Loads Grammar from File 'Grammar\MyGrammar.xaml'
        /// </summary>
        private async void InitializeSpeechRecognizer()
        {
            // Initialize SpeechRecognizer Object
            var myRecognizer = new SpeechRecognizer();

            // Register Event Handlers
            myRecognizer.StateChanged += MyRecognizer_StateChanged;
            myRecognizer.ContinuousRecognitionSession.ResultGenerated += MyRecognizer_ResultGenerated;

            // Create Grammar File Object
            var grammarContentFile = await Package.Current.InstalledLocation.GetFileAsync(@"Grammar\MyGrammar.xml");

            // Add Grammar Constraint from Grammar File
            var grammarConstraint = new SpeechRecognitionGrammarFileConstraint(grammarContentFile);
            myRecognizer.Constraints.Add(grammarConstraint);

            // Compile Grammar
            var compilationResult = await myRecognizer.CompileConstraintsAsync();

            // Write Debug Information
            Debug.WriteLine($"Status: {compilationResult.Status}");

            // If Compilation Successful, Start Continuous Recognition Session
            if (compilationResult.Status == SpeechRecognitionResultStatus.Success)
            {
                await myRecognizer.ContinuousRecognitionSession.StartAsync();
            }
        }

        /// <summary>
        /// Fires when MyRecognizer successfully parses a speech
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MyRecognizer_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            // Write Debug Information
            Debug.WriteLine(args.Result.Text);

            // Drive robot on recognized speech
            switch (args.Result.Text)
            {
                case "move forward":
                    _robot.MoveForward();
                    break;
                case "move reverse":
                    break;
                case "turn right":
                    break;
                case "turn left":
                    break;
                case "stop":
                    break;
            }

            // Turn on/off obstacle detection

        }

        /// <summary>
        /// Fires when MyRecognizer's state changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MyRecognizer_StateChanged(SpeechRecognizer sender, SpeechRecognizerStateChangedEventArgs args)
        {
            // Write Debug Information
            Debug.WriteLine(args.State);
        }
    }
}
