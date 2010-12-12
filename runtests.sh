#!/bin/bash
mono --runtime=v4.0.30319 ~/mono/NUnit-2.5.8.10295/bin/net-2.0/nunit-console.exe TaskListRESTService.Tests/bin/Debug/TaskListRESTService.Tests.dll TaskListDao.Tests/bin/Debug/TaskListDao.Tests.dll
pkill -f nunit-agent
