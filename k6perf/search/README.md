# Search endpoint perf tests

This file contains summary of perf tests for `/api/v1/user/search` endpoint. It contains test result before and after optimization.

## Optimization 
To optimize endpoint the index has been added.
```sql
CREATE INDEX idx_users_first_name_second_name_id ON users(first_name text_pattern_ops, second_name text_pattern_ops, id);
```

## Specification

| Spec          | Value            |
| ------------- | ---------------- |
| Table         | `users`          |
| Rows          | `1.000.000`      |
| Max Pool Size | `100`            |

## Stress tests
The stress test use ***10ms*** delay between requests for every VU.

| VUs  | Before optimization                                                                                                                                       | After optimization                                                                                                                                        |
| ---- | --------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 1    | latency p(95) = **684.2ms** (✅ <1000 ms)<br>latency p(90) = **633.54ms** (❌ <500 ms)<br><br>rps = **1.77**<br><br>failed requests = **0.00%** (✅ < 0.01%) | latency p(95) = **3.22ms** (✅ <1000 ms)<br>latency p(90) = **2.24ms** (✅ <500 ms)<br><br>rps = **82.57**<br><br>failed requests = **0.00%** (✅ < 0.01%)   |
| 10   | latency p(95) = **4990ms** (❌ <1000 ms)<br>latency p(90) = **4660ms** (❌ <500 ms)<br><br>rps = **3.18**<br><br>failed requests = **0.00%** (✅ < 0.01%)    | latency p(95) = **2.44ms** (✅ <1000 ms)<br>latency p(90) = **2.09ms** (✅ <500 ms)<br><br>rps = **607.52**<br><br>failed requests = **0.00%** (✅ < 0.01%)  |
| 100  | latency p(95) = **132ms** (✅ <1000 ms)<br>latency p(90) = **6.6ms** (✅ <500 ms)<br><br>rps = **104.54**<br><br>failed requests = **95.03%** (❌ < 0.01%)   | latency p(95) = **4.82ms** (✅ <1000 ms)<br>latency p(90) = **3.9ms** (✅ <500 ms)<br><br>rps = **5697.73**<br><br>failed requests = **0.00%** (✅ < 0.01%)  |
| 1000 | latency p(95) = **1750ms** (❌ <1000 ms)<br>latency p(90) = **1660ms** (❌ <500 ms)<br><br>rps = **775.74**<br><br>failed requests = **99.68%** (❌ < 0.01%) | latency p(95) = **1550ms** (❌ <1000 ms)<br>latency p(90) = **1290ms** (❌ <500 ms)<br><br>rps = **1477.9**<br><br>failed requests = **46.45%** (❌ < 0.01%) |

## Summary
The optimization provided a strong improvement in latency and RPS by accelerating query execution. For 100 VUs, the failed request rate dropped to zero, indicating that the previous bottleneck has disappeared at this load. However, at 1000 VUs, the failed requests remain high because the database connections are exhausted: 1k users hit the DB every 10ms, and the database cannot respond fast enough, queuing the next requests. For this load, further optimizations are needed. Overall, the index performs very well, drastically reducing latency and significantly increasing RPS.