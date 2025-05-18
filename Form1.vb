Public Class Form1

    Dim currentQuestion As Integer = 0
    Dim humanScore As Integer = 0
    Dim aiScore As Integer = 0
    Dim responseTime As Integer = 0
    Dim fastAnswerDetected As Boolean = False

    Dim questions As String() = {
    "What would you do if you saw a puppy stuck in a drain?",
    "Which word best defines 'humanity'?",
    "Complete the phrase: 'Roses are red, violets are blue, ______.'",
    "Describe rain in one word.",
    "What is 2 + 2?",
    "Someone drops their wallet in front of you. What do you do?",
    "Pick a favorite smell:",
    "How do you feel when someone cries in front of you?"
    }

    Dim answers As String(,) = {
    {"Rescue it immediately", "Take a photo", "Ignore it", "Analyze the situation"},
    {"Logic", "Compassion", "Efficiency", "Calculation"},
    {"algorithms are precise", "flowers bloom", "someone loves you", "system error"},
    {"peaceful", "wet", "data", "unclear"},
    {"context-dependent", "22", "not defined", "4"},
    {"Return it to them", "Keep it", "Scan for fingerprints", "Run analysis"},
    {"Nothing", "Gasoline", "Fresh bread", "I don’t process smell"},
    {"Feel awkwardComfort them", "Comfort them", "Record data", "Remain unaffected"}
    }

    ' {Human points, AI points}
    Dim scores As Integer(,,) = {
    {{5, 0}, {2, 3}, {1, 4}, {0, 5}},
    {{2, 3}, {5, 0}, {1, 4}, {0, 5}},
    {{0, 5}, {3, 2}, {5, 0}, {1, 4}},
    {{5, 0}, {2, 2}, {0, 5}, {1, 4}},
    {{1, 4}, {0, 5}, {2, 3}, {5, 0}},
    {{5, 0}, {0, 5}, {1, 4}, {2, 3}},
    {{1, 4}, {2, 3}, {5, 0}, {0, 5}},
    {{2, 3}, {5, 0}, {0, 5}, {1, 4}}
}
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadQuestion()
    End Sub

    Private Sub LoadQuestion()
        lblQuestion.Text = questions(currentQuestion)
        RadioButton1.Text = answers(currentQuestion, 0)
        RadioButton2.Text = answers(currentQuestion, 1)
        RadioButton3.Text = answers(currentQuestion, 2)
        RadioButton4.Text = answers(currentQuestion, 3)

        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        RadioButton4.Checked = False

        responseTime = 0
        responseTimer.Start()
    End Sub

    Private Sub btnNextQuestion_Click(sender As Object, e As EventArgs) Handles btnNextQuestion.Click
        Dim selected As Integer = -1
        If RadioButton1.Checked Then selected = 0
        If RadioButton2.Checked Then selected = 1
        If RadioButton3.Checked Then selected = 2
        If RadioButton4.Checked Then selected = 3

        If selected = -1 Then
            MessageBox.Show("Please select an answer.")
            Exit Sub
        End If

        responseTimer.Stop()
        If responseTime <= 2 Then
            aiScore += 2
            fastAnswerDetected = True
        End If

        humanScore += scores(currentQuestion, selected, 0)
        aiScore += scores(currentQuestion, selected, 1)

        currentQuestion += 1

        If currentQuestion < questions.Length Then
            LoadQuestion()
        Else
            ShowResult()
        End If
    End Sub

    Private Sub ShowResult()
        Dim resultText As String
        If humanScore > aiScore Then
            resultText = "You are human! 🧍 (" & humanScore & " : " & aiScore & ")"
        ElseIf aiScore > humanScore Then
            resultText = "Hmm... you might be an AI 🤖 (" & aiScore & " : " & humanScore & ")"
        Else
            resultText = "Undetermined. Maybe you're a cyborg? ⚙️"
        End If

        If fastAnswerDetected Then
            resultText &= vbCrLf & "⚠️ Rapid responses detected!"
        End If

        MessageBox.Show(resultText)
        btnNextQuestion.Enabled = False
    End Sub

    Private Sub responseTimer_Tick(sender As Object, e As EventArgs) Handles responseTimer.Tick
        responseTime += 1
    End Sub

End Class
