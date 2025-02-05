from flask import Flask, render_template, request, jsonify
import random

app = Flask(__name__)

# Sample topics and questions
TOPIC_SAMPLES = ["Nature", "Technology", "Science", "Art", "History", "Space", "Health", "Education", "Sports", "Music"]
QUESTION_SAMPLES = [
    "What is the significance of this image?",
    "How does this image relate to history?",
    "What technological aspect is depicted here?",
    "Explain the artistic elements in this image.",
    "Describe the scientific concept shown.",
    "What message does this image convey?",
    "How does this image relate to daily life?",
    "What emotions does this image evoke?",
    "What are the key elements in this image?",
    "Describe the background story of this image."
]

@app.route('/')
def index():
    return render_template('index.html')  # Ensure your HTML file is saved as 'templates/index.html'

@app.route('/generate_topics', methods=['POST'])
def generate_topics():
    return jsonify({"topics": random.sample(TOPIC_SAMPLES, 5)})

@app.route('/generate_questions', methods=['POST'])
def generate_questions():
    return jsonify({"questions": random.sample(QUESTION_SAMPLES, 10)})

@app.route('/generate_answer', methods=['POST'])
def generate_answer():
    question = request.form.get("question")
    return jsonify({"answer": f"This is a generated answer for the question: {question}"})

if __name__ == '__main__':
    app.run(debug=True)
