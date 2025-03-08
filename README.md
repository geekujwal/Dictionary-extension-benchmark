# Dictionary Benchmark

This project benchmarks various dictionary operations using **BenchmarkDotNet** to compare the performance of different methods for adding and updating key-value pairs.

## 📌 Setup

### **1. Install .NET SDK**
Ensure you have the .NET SDK installed. You can check by running:
```sh
dotnet --version
```
If not installed, download it from [dotnet.microsoft.com](https://dotnet.microsoft.com/).

### **2. Clone the Repository**
```sh
git clone <repository-url>
cd BenchmarkApp
```

### **3. Install Dependencies**
Run the following command to install **BenchmarkDotNet** (if not already installed):
```sh
dotnet add package BenchmarkDotNet
```

## 🚀 Running the Benchmark

### **1. Run in Release Mode (Recommended)**
```sh
dotnet run -c Release
```
This ensures the benchmark runs with optimizations enabled, providing accurate results.

### **2. Export Results (Optional)**
To save benchmark results in Markdown (`.md`) or JSON (`.json`) format:
```sh
dotnet run -c Release -- --exporters md,json
```

## 📊 Understanding the Results
| Method               | Mean   | Error   | StdDev  |
|----------------------|--------|--------|-------- |
| GetOrAdd_New        | X ns   | ± X ns | X ns   |
| GetOrAdd_Existing   | X ns   | ± X ns | X ns   |
| TryUpdate_Existing  | X ns   | ± X ns | X ns   |
| TryUpdate_New       | X ns   | ± X ns | X ns   |
| ContainsKey_Add     | X ns   | ± X ns | X ns   |
| TryGetValue_Add     | X ns   | ± X ns | X ns   |

- **Mean**: Average execution time.
- **Error**: Margin of error (based on 95% confidence interval).
- **StdDev**: Standard deviation, indicating variability between runs.

## 🏆 Best Practices
- **For adding/updating efficiently**: Use `GetOrAdd_New`.
- **For updating only existing keys**: Use `TryUpdate_Existing`.
- **Avoid `ContainsKey_Add`** (it’s the slowest approach).

## 📜 License
This project is licensed under the MIT License.

---
For any issues or contributions, feel free to submit a PR or open an issue! 🚀

