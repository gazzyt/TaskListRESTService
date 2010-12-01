#!/bin/bash
mono --runtime=v4.0.30319 ~/mono/NUnit-2.5.8.10295/bin/net-2.0/nunit-console.exe TaskListRESTService.nunit
pkill -f nunit-agent
