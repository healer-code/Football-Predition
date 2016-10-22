using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Prediction.Model
{
  public  class ANN_BackPropagation
    {
        #region Attributes of ANN one Hidden Layer
        /// <summary>
        /// Speed of Learning in Neutron NetWork
        /// </summary>
        protected double Learning_Rate;
        /// <summary>
        /// Number of time to train Data from user.
        /// </summary>
        public int Time_Training { get; set; }
        protected double[] Input_Weight, Hidden_Weight, Output_Weight, Final_Output;
        protected double[,] Weight_In, Weight_Out, Delta_Weight_In, Delta_Weight_Out;
        #endregion

        #region Defined matrix and value for ANN matrix
        /// <summary>
        /// Defined a Neutron Network which Value
        /// </summary>
        /// <param name="_Learning_Rate">Defined Learning Rate in ANN</param>
        /// <param name="_Time_Training">Defined Time to learn in ANN</param>
        /// <param name="_Neutron_In">Number of Neutron in Input Layer</param>
        /// <param name="_Neutron_Hidden">Number of Neutron in Hidden Layer</param>
        /// <param name="_Neutron_Output">Number of neutron in Output Layer</param>
        public ANN_BackPropagation(double _Learning_Rate, int _Time_Training, int _Neutron_Input, int _Neutron_Hidden, int _Neutron_Output)
        {
            Learning_Rate = _Learning_Rate;
            Time_Training = _Time_Training;
            Input_Weight = new double[_Neutron_Input];
            Hidden_Weight = new double[_Neutron_Hidden];
            Output_Weight = new double[_Neutron_Output];
            Final_Output = new double[_Neutron_Output];
            Weight_In = new double[_Neutron_Input, _Neutron_Hidden];
            Weight_Out = new double[_Neutron_Hidden, _Neutron_Output];
            Delta_Weight_In = new double[_Neutron_Input, _Neutron_Hidden];
            Delta_Weight_Out = new double[_Neutron_Hidden, _Neutron_Output];
            Initization();
        }
        /// <summary>
        /// Defined value for Delta Matrix in ANN
        /// </summary>
        void InitizationDeltaMatrix()
        {
            //Delta weight in Defined value
            for (int i = 0; i < Input_Weight.Length; i++)
                for (int j = 0; j < Hidden_Weight.Length; j++)
                    Delta_Weight_In[i, j] = 0;
            //Delta weight out Defined value
            for (int i = 0; i < Hidden_Weight.Length; i++)
                for (int j = 0; j < Output_Weight.Length; j++)
                    Delta_Weight_Out[i, j] = 0;
        }
        /// <summary>
        /// Defined value for weight-in and weight-out matrix in ANN
        /// </summary>
        public void Initization()
        {
            Random Value = new Random();
            //Random value weight for matrix representation weight from Input Layer to Hidden Layer
            for (int i = 0; i < Input_Weight.Length; i++)
                for (int j = 0; j < Hidden_Weight.Length; j++)
                    Weight_In[i, j] = Value.NextDouble() - 0.5;
            //Random value weight for matrix representation weight from Hidden Layer to Output Layer
            for (int i = 0; i < Hidden_Weight.Length; i++)
                for (int j = 0; j < Output_Weight.Length; j++)
                    Weight_Out[i, j] = Value.NextDouble() - 0.5;
            InitizationDeltaMatrix();
        }
        /// <summary>
        /// Add Training Data to ANN
        /// </summary>
        /// <param name="_input_weight">Double Array add to Input Weight</param>
        void SetInput(params double[] _input_weight)
        {
            for (int i = 0; i < _input_weight.Length; i++)
                Input_Weight[i] = _input_weight[i];
        }
        /// <summary>
        /// Add Value to Final Output
        /// </summary>
        /// <param name="_final_weight">Double Array add to Final Weight</param>
        void SetFinalWeightOutput(params double[] _final_weight)
        {
            for (int i = 0; i < _final_weight.Length; i++)
                Final_Output[i] = _final_weight[i];
        }
        #endregion

        #region Training and Learning Data
        /// <summary>
        /// Training from input layer to output layer
        /// </summary>
        void TrainingFoward()
        {
            //Training from input to hidden
            for (int i = 0; i < Hidden_Weight.Length; i++)
            {
                double sum_weight = 0;
                for (int j = 0; j < Input_Weight.Length; j++)
                    sum_weight += Input_Weight[j] * Weight_In[j, i];
                //using Transfer Function
                Hidden_Weight[i] = 1.0 / (1.0 + Math.Exp(-sum_weight));
            }
            //Training from hidden to output
            for (int i = 0; i < Output_Weight.Length; i++)
            {
                double sum_weight = 0;
                for (int j = 0; j < Hidden_Weight.Length; j++)
                    sum_weight += Hidden_Weight[j] * Weight_Out[j, i];
                Output_Weight[i] = 1.0 / (1.0 + Math.Exp(-sum_weight));
            }
        }
        /// <summary>
        /// Sumary error of Hidden and Ouput Layer
        /// </summary>
        void BackingFindError()
        {
            double[] OutputError = new double[Output_Weight.Length],
                     HiddenError = new double[Hidden_Weight.Length];
            //Find error of Ouput Layer
            for (int i = 0; i < Output_Weight.Length; i++)
                OutputError[i] = Output_Weight[i] * (1.0 - Output_Weight[i]) * (Final_Output[i] - Output_Weight[i]);
            //Find error of Hidden Layer 
            for (int i = 0; i < Hidden_Weight.Length; i++)
            {
                double total_error = 0;
                for (int j = 0; j < Output_Weight.Length; j++)
                    total_error += Weight_Out[i, j] * OutputError[j];
                HiddenError[i] = Hidden_Weight[i] * (1.0 - Hidden_Weight[i]) * total_error;
            }
            //Find delta error of Hidden
            for (int i = 0; i < Hidden_Weight.Length; i++)
                for (int j = 0; j < Output_Weight.Length; j++)
                    Delta_Weight_Out[i, j] += OutputError[j] * Hidden_Weight[i];
            //Find delta error of Input
            for (int i = 0; i < Input_Weight.Length; i++)
                for (int j = 0; j < Hidden_Weight.Length; j++)
                    Delta_Weight_In[i, j] += HiddenError[j] * Input_Weight[i];
        }
        /// <summary>
        /// Learning Training Data by change value of weight
        /// </summary>
        void LearningData()
        {
            //Learning Output
            for (int i = 0; i < Hidden_Weight.Length; i++)
                for (int j = 0; j < Output_Weight.Length; j++)
                    Weight_Out[i, j] += Learning_Rate * Delta_Weight_Out[i, j];
            //Learning Hidden
            for (int i = 0; i < Input_Weight.Length; i++)
                for (int j = 0; j < Hidden_Weight.Length; j++)
                    Weight_In[i, j] += Learning_Rate * Delta_Weight_In[i, j];
        }
        /// <summary>
        /// Get Training Data and start to training
        /// </summary>
        /// <param name="input_data">Training Data</param>
        /// <param name="final_output">Desired Value</param>
        public void TrainingData(double[][] input_data, double[][] final_output)
        {
            InitizationDeltaMatrix();
            for (int i = 0; i < input_data.Length; i++)
            {
                SetInput(input_data[i]);
                SetFinalWeightOutput(final_output[i]);
                TrainingFoward();
                BackingFindError();
            }
            LearningData();
        }
        #endregion

        #region Testing Data from user and print result
        /// <summary>
        /// Get output Data of ANN after training
        /// </summary>
        /// <returns></returns>
        double[] GetOutputData()
        {
            return (double[])Output_Weight.Clone();
        }
        /// <summary>
        /// Get Training test to run
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double[] TestingData(params double[] input)
        {
            SetInput(input);
            TrainingFoward();
            return GetOutputData();
        }

        #endregion
    }
}
