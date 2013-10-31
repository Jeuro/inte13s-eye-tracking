import clr
clr.AddReference("System.Windows.Forms")
clr.AddReference("System.Drawing")
 
#from System.Windows.Forms import MessageBox
#MessageBox.Show("Hello World")

from System.Windows.Forms import Form, TextBox, Button, FormWindowState, KeyEventArgs, Application, Keys
from System.Drawing import Point


class EmulateFnKeys(Form):

    # Variable for emulating fn-keys

    fnkey = None
    textbox = None
    button1 = None
    button2 = None

    def __init__(self):

        

        self.Text = "Function Keys"
        self.WindowState = FormWindowState.Maximized

        # Textbox

        self.textbox = TextBox()

        self.textbox.Focus()

        self.textbox.Location = Point(100, 100)

        # Buttons

        self.button1 = Button()

        self.button1.Text = "Button 1"

        self.button1.Location = Point(100, 150)

        self.button2 = Button()

        self.button2.Text = "Button 2"

        self.button2.Location = Point(250, 150)

        # Attaching handlers

        #textbox.KeyPress += self.KeyPressed
        self.textbox.KeyUp += self.KeyPressed

        self.button1.Click += self.btn1_Clicked

        self.button2.Click += self.btn2_Clicked

        self.Controls.Add(self.textbox)
        self.Controls.Add(self.button1)
        self.Controls.Add(self.button2)

    # Handler for button 1

    def btn1_Clicked(self, sender, event):

        self.fnkey = Keys.F1
        self.textbox.Text = ""

    # Handler for button 2

    def btn2_Clicked(self, sender, event):

        self.fnkey = Keys.F2
        self.textbox.Text = ""

    # Handler for textbox

    def KeyPressed(self, sender, args):
        
        #self.Text = "Pressed" + str(self.GetType())
        
        key = self.fnkey

        if key == Keys.F1:
            sender.Text = "F1 pressed"
        elif key == Keys.F2:
            sender.Text = "F2 pressed"
        elif key == Keys.F3:
            sender.Text = "F3 pressed"
        elif key == Keys.F4:
            sender.Text = "F4 pressed"
        elif key == Keys.F5:
            sender.Text = "F5 pressed"
        elif key == Keys.F6:
            sender.Text = "F6 pressed"
        elif key == Keys.F7:
            sender.Text = "F7 pressed"
        elif key == Keys.F8:
            sender.Text = "F8 pressed"
        elif key == Keys.F9:
            sender.Text = "F9 pressed"
        elif key == Keys.F10:
            sender.Text = "F10 pressed"
        elif key == Keys.F11:
            sender.Text = "F11 pressed"
        elif key == Keys.F12:
            sender.Text = "F12 pressed"

form = EmulateFnKeys()
Application.Run(form)