# BalancedTreeImplementations

**AVL Tree & Red-Black Tree** implementations in C# (generic, self-balancing, with insert/delete/search).

---

## 📌 Overview

This project contains two fully functional self-balancing binary search tree implementations:

- **AVL Tree** – maintains a strict balance factor (±1,0) using single/double rotations.
- **Red-Black Tree** – maintains balance with 5 rules and uses rotations + recoloring; handles all deletion cases (double‑black, sibling red/black, etc.).

Both implementations are **generic** (`<T>`) and work with any data type (`int`, `string`, custom classes, etc.).

---

## 🚀 Features

### AVL Tree
- Insertion & deletion with automatic rebalancing.
- Height tracking and balance factor calculation.
- Single (left/right) and double (left-right/right-left) rotations.
- In‑order traversal maintains sorted order.

### Red-Black Tree
- Insertion & deletion with full fix‑up logic.
- All 4 deletion cases (Case 1, 2.1, 2.2.1, 2.2.2) correctly handled.
- Double‑black resolution using a temporary dummy node (safe for reference types).
- Recursive search and node transplant.

### Both Trees
- `Insert(T value)`
- `Delete(T value)`
- `Find(T value) -> Node`
- `PrintTree()` – visual hierarchical output with colors for Red‑Black Tree.
- Generic – same code works for `int`, `string`, etc.

---

## 🛠️ Usage

### 1. Clone & Build
```bash
git clone https://github.com/Alco222/BalancedTreeImplementations.git
cd BalancedTreeImplementations
dotnet build
