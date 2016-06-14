using System;
using Adafruit.IoT.Devices;
using Adafruit.IoT.Motors;

namespace com.cdwaddell.sf7
{
    public class Robot
    {
        private readonly MotorHat2348 _controller;
        private readonly PwmDCMotor _leftMotor;
        private readonly PwmDCMotor _rightMotor;

        public Robot()
        {
            _controller = new MotorHat2348();

            _leftMotor = _controller.CreateDCMotor(1);
            _leftMotor.SetSpeed(60);

            _rightMotor = _controller.CreateDCMotor(2);
            _rightMotor.SetSpeed(60);
        }

        public void MoveForward()
        {
            _leftMotor.Run(Direction.Forward);
            _rightMotor.Run(Direction.Forward);
        }
    }
}
