cmd /k k6 run --env BASE_URL=http://localhost:5000 ^
        --env AUTH_TOKEN=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImQ0YmQwMWE2LWI5MDUtNDAwNy05NTA2LTE4MTcxNDEzYzFkNiIsImV4cCI6NDg4MzE0NjU1NH0.ZCFMCJT5Cb6RgzKVN-F6A5p3y-EBS-3Vgzl2VeQkRaA ^
        --env TARGET_VUS=10 ^
        --env DURATION=30s ^
        --env MODE=realistic ^
        ..\search.ts