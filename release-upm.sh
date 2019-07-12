  git subtree split --prefix="Assets/UniRx.Async" --branch upm
  git tag upm-$1 upm
  git push origin upm --tags