using System;
using System.Windows.Forms;

namespace GrammarParser
{
    public partial class Form1 : Form
    {
        private int index = 0; // Tracks the position of the input string
        private string input = ""; // Input string to be parsed

        public Form1()
        {
            InitializeComponent();
        }

        // Add the Form1_Load event handler
        private void Form1_Load(object sender, EventArgs e)
        {
            // You can initialize form components here if needed.
        }

        // Grammar S → E
        private bool S()
        {
            return E();
        }

        // Grammar E → E + T | T
        private bool E()
        {
            int startPos = index;

            if (T())
            {
                if (index < input.Length && input[index] == '+')
                {
                    index++;
                    if (E()) return true;
                }
                else
                {
                    return true; // Return true as T is valid
                }
            }

            index = startPos; // Backtrack
            return false;
        }

        // Grammar T → T * F | F
        private bool T()
        {
            int startPos = index;

            if (F())
            {
                if (index < input.Length && input[index] == '*')
                {
                    index++;
                    if (T()) return true;
                }
                else
                {
                    return true; // Return true as F is valid
                }
            }

            index = startPos; // Backtrack
            return false;
        }

        // Grammar F → ( E ) | id
        private bool F()
        {
            int startPos = index;

            if (index < input.Length && input[index] == '(')
            {
                index++;
                if (E())
                {
                    if (index < input.Length && input[index] == ')')
                    {
                        index++;
                        return true;
                    }
                }
            }
            else if (index < input.Length && char.IsLetter(input[index])) // id is represented by a letter
            {
                index++;
                return true;
            }

            index = startPos; // Backtrack
            return false;
        }

        // This method is triggered when the "Parse" button is clicked
        private void parseButton_Click(object sender, EventArgs e)
        {
            input = inputTextBox.Text.Replace(" ", ""); // Remove spaces for easier parsing
            index = 0; // Reset index

            if (S() && index == input.Length) // Check if the entire input matches
            {
                resultLabel.Text = "Valid input.";
                resultLabel.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                resultLabel.Text = "Invalid input.";
                resultLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
