import pandas as pd
from sklearn.tree import DecisionTreeClassifier
import numpy as np

# Load training data with Risk and Skill classifications
data = pd.read_csv("training_data.csv")

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

# Make predictions for a new scenario
new_scenario = [[0, 2, 2, 1, 1, 1]]  # Odds=0, Stakes=2, Rerolls=2, Killshot=True, Panic=True, Decision=True
risk_prediction = risk_tree.predict(new_scenario)[0]
skill_prediction = skill_tree.predict(new_scenario)[0]

print(f"\nPrediction for new scenario:")
print(f"Risk: {'Risky' if risk_prediction == 1 else 'Turtle'}")
print(f"Skill: {'Skillful' if skill_prediction == 1 else 'Newbie'}")

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

# Example usage
print(f"\nExample classification:")
risk_class, skill_class = classify_move(0, 5, 2, True, False, True)
print(f"Move classified as: {risk_class} / {skill_class}")