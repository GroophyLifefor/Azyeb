import React from 'react';
import clsx from 'clsx';
import styles from './styles.module.css';

const FeatureList = [
  {
    title: 'Easy to Use',
    Svg: require('@site/static/img/soon.svg').default,
    description: (
      <>
        Azyeb gives quick results in the format you want with simple syntax.
      </>
    ),
  },
  {
    title: 'Focus on What Matters',
    Svg: require('@site/static/img/soon.svg').default,
    description: (
      <>
        It is now much easier to adapt the given formats to your program, focus on how you will use the data.
      </>
    ),
  },
  {
    title: 'Powered by Mathos-Parser',
    Svg: require('@site/static/img/mathos.svg').default,
    description: (
      <>
        Mathos Parser is a mathematical expression parser and evaluator for the .NET Standard. It can parse various kinds of mathematical expressions out of the box and can be extended with custom functions, operators, and variables.
      </>
    ),
  },
];

function Feature({Svg, title, description}) {
  return (
    <div className={clsx('col col--4')}>
      <div className="text--center">
        <Svg className={styles.featureSvg} role="img" />
      </div>
      <div className="text--center padding-horiz--md">
        <h3>{title}</h3>
        <p>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures() {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}
