#!/usr/bin/env bash

set -eo pipefail

npm install
npm run $@
