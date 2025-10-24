# Dice Game Decision Tree Analyzer

This project creates a machine learning decision tree to analyze dice game decisions based on the provided CSV data.

## Features

- **Data Preprocessing**: Cleans and prepares the CSV data for machine learning
- **Decision Tree Training**: Creates an optimized decision tree classifier
- **Visualization**: Generates visual representations of the decision tree
- **Feature Importance Analysis**: Shows which features are most important for decisions
- **Prediction System**: Allows you to make predictions for new scenarios
- **Text Rules**: Displays the decision tree rules in readable text format

## Files

- `Analyser.py` - Main analysis class with all functionality
- `data.csv` - Your dice game data
- `example_usage.py` - Example script showing how to use the analyser
- `requirements.txt` - Python dependencies
- `README.md` - This documentation

## Installation

1. Install the required Python packages:
```bash
pip install -r requirements.txt
```

## Usage

### Quick Start

Run the main analysis:
```bash
python Analyser.py
```

### Custom Usage

```python
from Analyser import DiceGameAnalyser

# Create analyser instance
analyser = DiceGameAnalyser("data.csv")

# Run complete analysis
analyser.run_complete_analysis()

# Make predictions
analyser.predict_decision(odds=0, stakes=2, rerolls=2, killshot=True, panic=True)
```

### Example Scenarios

Run the example usage script:
```bash
python example_usage.py
```

## Data Format

The CSV file should have the following columns:
- `Odds`: Numeric value
- `Stakes`: Numeric value (betting amount)
- `Rerolls`: Numeric value (number of rerolls)
- `Killshot`: Boolean (True/False)
- `Panic`: Boolean (True/False)
- `Decision`: Boolean (True/False) - This is the target variable

## Output

The analyzer will provide:

1. **Data Summary**: Information about the loaded dataset
2. **Model Performance**: Accuracy, classification report, and confusion matrix
3. **Decision Tree Rules**: Text-based rules showing the decision logic
4. **Feature Importance**: Ranking of which features are most important
5. **Tree Visualization**: Graphical representation of the decision tree
6. **Predictions**: Results for test scenarios

## Understanding the Results

- **Feature Importance**: Shows which game mechanics (Stakes, Rerolls, Killshot, etc.) are most influential in decision-making
- **Decision Tree Rules**: Provides clear if-then logic for how decisions are made
- **Predictions**: Allows you to test new scenarios and see what the model would decide

## Customization

You can modify the decision tree parameters in the `train_decision_tree` method:
- `max_depth`: Maximum depth of the tree
- `min_samples_split`: Minimum samples required to split a node
- `min_samples_leaf`: Minimum samples required in a leaf node

## Notes

- The data is automatically cleaned to remove outliers (e.g., extremely large stake values)
- Boolean values are converted to integers (0/1) for machine learning compatibility
- The model uses stratified train-test split to ensure balanced representation
