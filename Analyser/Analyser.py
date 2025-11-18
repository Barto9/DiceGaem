import pandas as pd
from sklearn.tree import DecisionTreeClassifier
import numpy as np

# Load training data with Risk and Skill classifications
data = pd.read_csv("training_data.csv", sep=";")

# Clean the data - remove the weird rows with huge numbers
data = data[data['Stakes'] <= 100]

# Convert True/False to 1/0 for the computer
data['Killshot'] = data['Killshot'].astype(int)
data['Panic'] = data['Panic'].astype(int)
data['Decision'] = data['Decision'].astype(int)
data['Risk'] = data['Risk'].astype(int)
data['Skill'] = data['Skill'].astype(int)

# Separate features (what we use to predict) from targets (what we want to predict)
features = ['Odds', 'Stakes', 'Rerolls', 'Killshot', 'Panic', 'Decision']
X = data[features]
y_risk = data['Risk']
y_skill = data['Skill']

# Create and train decision trees for both Risk and Skill classification
risk_tree = DecisionTreeClassifier()
skill_tree = DecisionTreeClassifier()

risk_tree.fit(X, y_risk)
skill_tree.fit(X, y_skill)

# Show which features are most important for Risk classification
print("Risk Classification - Feature importance:")
for i, feature in enumerate(features):
    print(f"{feature}: {risk_tree.feature_importances_[i]:.3f}")

print("\nSkill Classification - Feature importance:")
for i, feature in enumerate(features):
    print(f"{feature}: {skill_tree.feature_importances_[i]:.3f}")

# Load and process move_data.csv for predictions
print("\n" + "="*60)
print("Predictions for scenarios in move_data.csv")
print("="*60)

# Check if we have any data to predict
try:
    move_data = pd.read_csv("move_data.csv", sep=";")
except FileNotFoundError:
    print("Error: move_data.csv not found!")
    move_data = pd.DataFrame()
except Exception as e:
    print(f"Error reading move_data.csv: {e}")
    move_data = pd.DataFrame()

# Validate that required columns exist
required_cols = ['Odds', 'Stakes', 'Rerolls', 'Killshot', 'Panic', 'Decision']
if len(move_data) > 0:
    missing_cols = set(required_cols) - set(move_data.columns)
    if missing_cols:
        raise ValueError(f"move_data.csv is missing required columns: {missing_cols}. "
                         f"Found columns: {list(move_data.columns)}. "
                         f"Make sure the file uses semicolons (;) as separators.")

# Clean the data - remove rows with huge numbers and empty rows
if len(move_data) > 0:
    move_data = move_data.dropna(subset=required_cols)  # Remove empty rows
    move_data = move_data[move_data['Stakes'] <= 100]  # Remove rows with huge stakes

    # Convert numeric columns properly (handle string values)
    numeric_cols = ['Odds', 'Stakes', 'Rerolls']
    for col in numeric_cols:
        if move_data[col].dtype == object:
            # Replace comma with dot for decimal values
            move_data[col] = move_data[col].astype(str).str.replace(',', '.', regex=False)
        move_data[col] = pd.to_numeric(move_data[col], errors='coerce')

    # Remove rows where numeric conversion failed
    move_data = move_data.dropna(subset=numeric_cols)

    # Convert True/False to 1/0 for boolean columns
    move_data['Killshot'] = move_data['Killshot'].astype(int)
    move_data['Panic'] = move_data['Panic'].astype(int)
    move_data['Decision'] = move_data['Decision'].astype(int)

# Check if we have any valid data after cleaning
if len(move_data) == 0:
    print("\nWarning: No valid data rows found in move_data.csv after cleaning!")
    print("Skipping predictions.")
else:
    # Prepare features for prediction
    X_move = move_data[features]

    # Make predictions for all scenarios
    risk_predictions = risk_tree.predict(X_move)
    skill_predictions = skill_tree.predict(X_move)

    # Display results
    print(f"\nTotal scenarios to predict: {len(move_data)}")
    print("\nPredictions:")
    print("-" * 60)
    print(f"{'Row':<6} {'Odds':<6} {'Stakes':<8} {'Rerolls':<8} {'Killshot':<10} {'Panic':<8} {'Decision':<10} {'Risk':<8} {'Skill':<10}")
    print("-" * 60)

    for idx, (i, row) in enumerate(move_data.iterrows()):
        risk_label = "Risky" if risk_predictions[idx] == 1 else "Turtle"
        skill_label = "Skillful" if skill_predictions[idx] == 1 else "Newbie"
        
        print(f"{idx+1:<6} {row['Odds']:<6.2f} {row['Stakes']:<8.0f} {row['Rerolls']:<8.0f} "
              f"{bool(row['Killshot']):<10} {bool(row['Panic']):<8} {bool(row['Decision']):<10} "
              f"{risk_label:<8} {skill_label:<10}")

    print("-" * 60)

# Function to classify a move
def classify_move(odds, stakes, rerolls, killshot, panic, decision):
    """
    Classify a move on both Risk and Skill axes
    Returns: (risk_classification, skill_classification)
    """
    scenario = [[odds, stakes, rerolls, int(killshot), int(panic), int(decision)]]
    risk_pred = risk_tree.predict(scenario)[0]
    skill_pred = skill_tree.predict(scenario)[0]
    
    risk_label = "Risky" if risk_pred == 1 else "Turtle"
    skill_label = "Skillful" if skill_pred == 1 else "Newbie"
    
    return risk_label, skill_label