@echo off
echo Installing required packages for Dice Game Decision Tree Analyzer...
echo.

pip install pandas>=1.3.0
pip install numpy>=1.21.0
pip install scikit-learn>=1.0.0
pip install matplotlib>=3.4.0
pip install seaborn>=0.11.0

echo.
echo Installation complete!
echo You can now run: python Analyser.py
pause
