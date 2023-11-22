using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TracNghiem_BTlop
{
    public partial class Form1 : Form
    {
        private Label[] questionLabels;
        private int currentQuestionIndex;
        private RadioButton[] answerOptions;
        private bool[][] questionStates;
        private Dictionary<int, int> radioStates = new Dictionary<int, int>();

        public Form1()
        {
            InitializeComponent();

            // Khởi tạo mảng các nhãn câu hỏi
            questionLabels = new Label[]
    {
        lb_1,
        lb_2,
        lb_3,
        lb_4,
        lb_5
    };

            // Khởi tạo mảng các nút radio
            answerOptions = new RadioButton[]
    {
        rbtn_A,
        rbtn_B,
        rbtn_C,
        rbtn_D
    };
            // Khởi tạo mảng trạng thái cho các nút radio
            questionStates = new bool[questionLabels.Length][];
            for (int i = 0; i < questionLabels.Length; i++)
            {
                questionStates[i] = new bool[answerOptions.Length];
            }

            // Ẩn tất cả các nhãn câu hỏi khi form được tải lên  
            HideAllQuestionLabels();
            questionLabels[0].Visible = true;

            // Khởi tạo chỉ số câu hỏi hiện tại
            currentQuestionIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRadioState();
            SaveRadioState();
            //questionLabels[currentQuestionIndex].Visible = true;
        }

        private void HideAllQuestionLabels()
        {
            foreach (Label questionLabel in questionLabels)
            {
                questionLabel.Visible = false;
            }
        }

        private void ClearAllSelections()
        {
            foreach (RadioButton radioButton in answerOptions)
            {
                radioButton.Checked = false;
            }
        }

        private void SaveRadioState()
        {
            for (int i = 0; i < answerOptions.Length; i++)
            {
                questionStates[currentQuestionIndex][i] = answerOptions[i].Checked;
            }
        }

        private void LoadRadioState()
        {
            for (int i = 0; i < answerOptions.Length; i++)
            {
                answerOptions[i].CheckedChanged -= RadioButton_CheckedChanged;
                answerOptions[i].Checked = questionStates[currentQuestionIndex][i];
                answerOptions[i].CheckedChanged += RadioButton_CheckedChanged;
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                // Lưu trạng thái của nút radio đã được chọn
                int answerIndex = Array.IndexOf(answerOptions, radioButton);
                questionStates[currentQuestionIndex][answerIndex] = true;
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {   
            // Ẩn câu hỏi hiện tại
            questionLabels[currentQuestionIndex].Visible = false;

            // Đặt lại trạng thái của các nút radio
            ClearAllSelections();

            // Tăng chỉ số câu hỏi hiện tại
            currentQuestionIndex++;

            // Kiểm tra nếu đã đạt đến câu hỏi cuối cùng,
            // thì không tăng chỉ số nữa và hiển thị câu hỏi cuối cùng
            if (currentQuestionIndex >= questionLabels.Length)
            {
                currentQuestionIndex = questionLabels.Length - 1;
            }

            // Hiển thị câu hỏi tiếp theo
            questionLabels[currentQuestionIndex].Visible = true;

            // Tải lại trạng thái của các nút radio
            LoadRadioState();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            // Lưu trạng thái của các nút radio
            SaveRadioState();

            // Giảm chỉ số câu hỏi hiện tại
            currentQuestionIndex--;

            // Kiểm tra nếu đã đạt đến câu hỏi đầu tiên,
            // thì không giảm chỉ số nữa và hiển thị câu hỏi đầu tiên
            if (currentQuestionIndex < 0)
            {
                currentQuestionIndex = 0;
            }

            // Ẩn câu hỏi hiện tại
            questionLabels[currentQuestionIndex + 1].Visible = false;

            // Hiển thị câu hỏi trước đó
            questionLabels[currentQuestionIndex].Visible = true;

            // Tải lại trạng thái của các nút radio```csharp
            LoadRadioState();
        }
    }
}
